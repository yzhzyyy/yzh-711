using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace server
{
    public partial class Server : Form
    {
        private string _selectedFile;
        public Server()
        {
            InitializeComponent();
            IPAddress iPAddr = IPAddress.Loopback;
            int port_cs = 8083;

            TcpListener listener = new TcpListener(iPAddr, port_cs);
            listener.Start();

            serverStatus1.Text= "Server listening on" + iPAddr + ":" + port_cs;

            Thread listenerThread = new Thread(() => StartListener(listener));
            listenerThread.IsBackground = true; // 设置为后台线程，以便在关闭应用程序时自动结束
            listenerThread.Start();

            // 设置allData文件夹的内容
            string dataPath1 = "../../../allData";
            string[] allFileList = Directory.GetFiles(dataPath1);
            string[] allFileListName = new string[allFileList.Length];
            for (int i = 0; i < allFileList.Length; i++)
            {
                allFileListName[i] = Path.GetFileName(allFileList[i]);
            }
            string fileList1 = string.Join(", ", allFileListName);
            allList.Items.AddRange(allFileListName.ToArray());

            // 设置Data文件夹的内容
            string dataPath2 = "../../../data";
            string[] dataList = Directory.GetFiles(dataPath2);
            string[] dataListName = new string[dataList.Length];
            for (int i = 0; i < dataList.Length; i++)
            {
                dataListName[i] = Path.GetFileName(dataList[i]);
            }
            string fileList2 = string.Join(", ", allFileListName);
            listBox1.Items.AddRange(dataListName.ToArray());

        }

        private void StartListener(TcpListener listener)
        {
            while (true)
            {
                var client = listener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(state => HandleClient(client));
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream_cs = client.GetStream();
                byte command = (byte)stream_cs.ReadByte();

                Invoke(new Action(() =>
                {
                    serverStatus2.Text = "cache connected";
                }));

                if (command == 0)
                {
                    StreamWriter writer_cs = new StreamWriter(stream_cs, Encoding.UTF8);

                    string[] fileList = Directory.GetFiles("../../../data");
                    string[] fileName = new string[fileList.Length];// 将文件列表拼接成字符串传入stream中

                    //对回去的文件列表进行操作，实现只展示文件名，不展示文件路径
                    for (int i = 0; i < fileList.Length; i++)
                    {
                        fileName[i] = Path.GetFileName(fileList[i]);
                    }
                    string fileList1 = string.Join(", ", fileName);
                    writer_cs.Write(fileList1);
                    writer_cs.Flush();
                }

                else if (command == 1)
                {
                    byte[] data = new byte[4];
                    stream_cs.Read(data, 0, 4);

                    // 从data数组的起始位0读取四个字节
                    int fileNameBytesLength = BitConverter.ToInt32(data, 0);
                    data = new byte[fileNameBytesLength];

                    // 再次从data的0位开始存储，读取流中fileNameBytesLength长度的内容
                    stream_cs.Read(data, 0, fileNameBytesLength);

                    // 获取文件路径
                    string fileName = Encoding.UTF8.GetString(data);
                    string currentDirectory = Directory.GetCurrentDirectory();

                    string grandParentDirectory = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", ".."));
                    string fileNamepath = Path.Combine(grandParentDirectory, "data", fileName);
                   

                    //////////////////////////////// 此处为对cacheList.txt的操作


                    // 将cache要获取的文件存入cacheList.txt中 ----> 要有一步判断，如果cacheList中有一样的hash值，则跳过，否则将hash值存入cacheList中，一行一个hash，记录cache中存在的block
                    // 在dataList中查找与fileName同名的文件夹，进入到这个文件夹下
                    string dataListPath = "../../../dataList";

                    string dataFolderPath = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "dataList", fileName));
                    string[] filePaths = Directory.GetFiles(dataFolderPath);

                    string cacheListPath = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "cacheList.txt"));

                    // 这是对的
                    // 遍历这个文件夹下的每一个文件，计算其哈希值，并将这个hash值记录到cacheList.txt中
                    int filePathLength = filePaths.Length;
                    byte[] filePathLengthByte = BitConverter.GetBytes(filePathLength);

                    stream_cs.Write(filePathLengthByte, 0, 4);

                    for (int i = 1; i <= filePaths.Length; i++)
                    {

                        try
                        {
                            string filePath = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "dataList", fileName, i.ToString() + ".txt"));
                           
                            byte[] messageBytes = File.ReadAllBytes(filePath);// messageBytes表示字节形式的文件内容
                            SHA256 sha256 = SHA256.Create();
                            byte[] hashBytes = sha256.ComputeHash(messageBytes);
                            string hashString = BitConverter.ToString(hashBytes).Replace("-", "");
                            // 数据覆盖了

                            bool hashExists = false;

                            // 判断cacheList文件中是否有这个哈希值

                            using (StreamReader reader = new StreamReader(cacheListPath))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (line == hashString)
                                    {
                                        hashExists = true;
                                        break;
                                    }
                                }
                            }

                            byte specifier;
                            if (hashExists)
                            {
                                specifier = 0; // 0表示存在
                                stream_cs.WriteByte(specifier);
                                stream_cs.Write(hashBytes); // 写回hashBytes

                                
                            }
                            // 如果没有，则将block内容传回cache,并在cacheList文件中添加这段hash值

                            else
                            {
                                specifier = 1; // 1表示不存在
                                stream_cs.WriteByte(specifier); // 将标识符写入流中
                                stream_cs.Write(hashBytes);

                                int blockSize = messageBytes.Length;
                                byte[] blockSizeByte = BitConverter.GetBytes(blockSize);
                                stream_cs.Write(blockSizeByte);
                                
                                stream_cs.Write(messageBytes);

                                // 写入cacheList文件
                                using (StreamWriter writer = new StreamWriter(cacheListPath, true))
                                {
                                    writer.WriteLine(hashString);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Invoke(new Action(() =>
                            {
                                label2.Text = "Error: " + e.Message;
                            }));
                        }

                    } 
                }
                else if(command == 3)
                {
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string cacheListPath = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "cacheList.txt"));
                    string cache11Path = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "cache11.txt"));
                    string content = File.ReadAllText(cache11Path);
                    File.WriteAllText(cacheListPath, content);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }            
        }
        private void serverStatus1_Click(object sender, EventArgs e)
        {
            
        }

        private void serverStatus2_Click(object sender, EventArgs e)
        {

        }

        private void list_Click(object sender, EventArgs e)
        {

        }

        private void allList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedFile = allList.SelectedItem as string ?? string.Empty;
            selectedFile.Text = _selectedFile;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string dataPath1 = "../../../allData"; 
                dataPath1 = Path.Combine(dataPath1, _selectedFile);// allData文件夹中选中文件的的路径
                string dataPath2 = "../../../data"; 
                dataPath2 = Path.Combine(dataPath2, _selectedFile);// data文件夹中选中文件的路径
                string dataPath3 = "../../../dataList";// dataList文件夹路径

                // 创建一个新文件夹，以选中的文件命名
                string newFolder = Path.Combine(dataPath3, _selectedFile); // 新文件夹的路径
                Directory.CreateDirectory(newFolder);

                File.Copy(dataPath1, dataPath2, overwrite: true);
                if (!listBox1.Items.Contains(_selectedFile))
                {
                    listBox1.Items.Add(_selectedFile);
                }


                // 将选中的可下载文件切片处理 ----> 存入datalist文件夹中
                List<byte[]> Robin_file = RabinFunction(dataPath2);
                int fileCounter = 1;
                foreach (byte[] data in Robin_file)
                {
                    string fileName = $"{fileCounter}.txt";
                    string filePath = Path.Combine(newFolder, fileName);
                    string hexString = ByteArrayToHexString(data);
                    File.WriteAllText(filePath, hexString);
                    fileCounter++;
                }
                Invoke(new Action(() =>
                {
                    selectedFile.Text = "Success Slicing";
                }));
                

            }
            catch(Exception ex) { 
                Console.Write(ex.ToString());
            }
            
        }

        private static string ByteArrayToHexString(byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder(byteArray.Length * 2);
            foreach (byte b in byteArray)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }


        // 罗宾函数,用于文件切片
        private static List<byte[]> RabinFunction(string filePath)
        {
            byte[] fileData = File.ReadAllBytes(filePath);

            List<byte[]> chunks = new List<byte[]>();
            List<byte[]> window = new List<byte[]>();
            int currentHash = 0;
            byte[] code = new byte[8];
            int chunkStart = 0;

            for (int i = 0; i < fileData.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i + j < fileData.Length)
                    {
                        code[j] = fileData[i + j];
                    }
                }
                if (Hash(code) == 0)
                {
                    int chunkSize = i - chunkStart + 1;
                    byte[] chunkData = new byte[chunkSize];
                    Buffer.BlockCopy(fileData, chunkStart, chunkData, 0, chunkSize);
                    chunks.Add(chunkData);
                    chunkStart = i + 1;

                }

            }
            if (chunkStart < fileData.Length)
            {
                int chunkSize = fileData.Length - chunkStart;
                byte[] chunkData = new byte[chunkSize];
                Buffer.BlockCopy(fileData, chunkStart, chunkData, 0, chunkSize);
                chunks.Add(chunkData);
            }

            return chunks;
        }
        private static int Hash(byte[] code)
        {
            int hash = 0;
            foreach (byte b in code)
            {
                hash = ((hash * 17 + (int)b + 75)) % 5003;
            }
            return hash % 1024;
        }


    }
}