using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

namespace cache
{
    public partial class Cache : Form
    {
        private TcpClient _tcpClient;
        private IPAddress _ipAddr_cs = IPAddress.Loopback;
        private int _port_cs = 8083;
        public Cache()
        {
            InitializeComponent();
            cacheStatus1.Text = "Cache starting!";

            // client与cache之间的链接
            IPAddress ipAddr_cc = IPAddress.Loopback;

            int port_cc = 8082;

            TcpListener listener = new TcpListener(ipAddr_cc, port_cc);
            listener.Start();

            cacheStatus2.Text = "Cache listening on:" + ipAddr_cc + ":" + port_cc;
            // cache和server之间的链接



            cacheStatus3.Text = "connected to server";
            Thread listenerThread = new Thread(() => StartListener(listener));
            listenerThread.IsBackground = true; // 设置为后台线程，以便在关闭应用程序时自动结束
            listenerThread.Start();
        }
        private void StartListener(TcpListener listener)
        {
            while (true)
            {
                var client = listener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(state => HandleCacheClient(client));
            }
        }

        private void HandleCacheClient(TcpClient client)
        {
            try
            {
                // catch stream from client
                NetworkStream stream_cc = client.GetStream();
                byte command = (byte)stream_cc.ReadByte();
                
                if (command == 0)
                {
                    _tcpClient = new TcpClient(_ipAddr_cs.ToString(), _port_cs);
                    using (NetworkStream stream_cs = _tcpClient.GetStream()) // 会在代码结束之后自动关闭
                    {
                        stream_cs.WriteByte(command);
                        stream_cs.Flush();

                        StreamReader reader_cs = new StreamReader(stream_cs, Encoding.UTF8);
                        StreamWriter writer_cc = new StreamWriter(stream_cc, Encoding.UTF8);

                        writer_cc.Write(reader_cs.ReadToEnd());
                        writer_cc.Flush();

                    }
                }
                else if (command == 1)
                {
                    _tcpClient = new TcpClient(_ipAddr_cs.ToString(), _port_cs);

                    byte[] data = new byte[4];
                    byte[] fileNameLengthByte = data; // 字节形式的文件名长度

                    // 从0位开始往后读四位存入data里
                    stream_cc.Read(data, 0, 4);

                    // 获取文件名长度
                    int fileNameLength = BitConverter.ToInt32(data, 0);

                    // 为存储文件名开拓空间
                    data = new byte[fileNameLength];

                    // 再次从data的0位开始存储，读取流中fileNameBytesLength长度的内容
                    stream_cc.Read(data, 0, fileNameLength);
                    byte[] fileNameByte = data;

                    // 获取文件路径
                    string fileName = Encoding.UTF8.GetString(data);

                    string currentDirectory = Directory.GetCurrentDirectory();

                    string grandParentDirectory = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", ".."));

                    int cacheHas = 0;
                    int cacheNotHas = 0;

                    // 对日志部分进行变量定义

                    // 获取log路径
                    string logPath = Path.Combine(grandParentDirectory, "log.txt");
                    // 获取时间戳
                    string timestamp = DateTime.Now.ToString("HH:mm:ss yyyy-mm-dd");

                    using (NetworkStream stream_cs = _tcpClient.GetStream())
                    {
                        // 向server发送请求
                        stream_cs.WriteByte(command);
                        stream_cs.Flush();

                        stream_cs.Write(fileNameLengthByte, 0, fileNameLengthByte.Length);
                        stream_cs.Write(fileNameByte, 0, fileNameByte.Length);
                        stream_cs.Flush();
                        Invoke(new Action(() =>
                        {
                            textBox1.Text = "";
                        }));
                        // 读取sever发来的文件
                        // 获取第一位specifier进行判断

                        //要先传入后面读几遍
                        byte[] data1= new byte[4];
                        stream_cs.Read(data1, 0, 4);
                        stream_cc.Write(data1, 0, 4);
                        int streamLength = BitConverter.ToInt32(data1);
                        for (int i=0; i < streamLength; i++)
                        {
                            byte specifier = (byte)stream_cs.ReadByte();
                            if (specifier == 0) // cache里有
                            {
                                data1 = new byte[32];
                                stream_cs.Read(data1, 0, 32);
                                string hashString = BitConverter.ToString(data1).Replace("-", "");
                                string dataPath = Path.GetFullPath(Path.Combine(grandParentDirectory, "data"));
                                string[] files = Directory.GetFiles(dataPath);
                                byte[] fileContent;
                                
                                foreach (string file in files)
                                {
                                    if (Path.GetFileName(file) == hashString + ".txt")
                                    {
                                        fileContent = File.ReadAllBytes(file); // 这里的fileContent是十六进制
                                        int contentSize = fileContent.Length;
                                        byte[] contentSizeByte = new byte[4];
                                        contentSizeByte = BitConverter.GetBytes(contentSize);
                                        stream_cc.Write(contentSizeByte);
                                        stream_cc.Write(fileContent);
                                        Invoke(new Action(() =>
                                        {
                                            textBox1.Text += $"{hashString}\n";
                                        }));
                                        break;
                                    }
                                }

                                // 对client进一步操作
                                // fileContent = FilterHexCharacters(fileContent);
                                //byte[] fileByte = HexStringToByteArray(fileContent);
                                
                                // 其他使用 fileContent 的代码
                                cacheHas++;
                            }

                            // cache里没有
                            else if (specifier == 1)
                            {
                                data1 = new byte[32];
                                stream_cs.Read(data1, 0, 32);
                                string hashString = BitConverter.ToString(data1).Replace("-", "");
                                string dataPath = Path.GetFullPath(Path.Combine(grandParentDirectory, "data"));
                                
                                // 读block的大小
                                byte[] data2 = new byte[4];
                                stream_cs.Read(data2, 0, 4);
                                int blockSize = BitConverter.ToInt32(data2);

                                //读block的内容
                                byte[] data3 = new byte[blockSize];
                                stream_cs.Read(data3, 0, blockSize);
                                string blockContent = ByteArrayToHexString(data3); // 缓存里存的是十六进制
                                string blockPath = Path.GetFullPath(Path.Combine(dataPath, hashString + ".txt"));

                                File.WriteAllBytes(blockPath, data3);
                                Invoke(new Action(() =>
                                {
                                    textBox1.Text += $"{blockContent}";
                                }));

                                // 写回client

                                stream_cc.Write(data2); 
                                stream_cc.Write(data3); // 往回写的是文件内容的byte

                                cacheNotHas++;
                            }
                        }
                    }

                    // 更新log
                    double reuse = (double)cacheHas / (cacheHas + cacheNotHas);
                    double reusePre = reuse * 100;
                        string logMessage = "user request: file " + fileName + " at " + timestamp + ". \n" + "response: " + $"{reusePre}%" +" of file " + fileName + " was constructed with the cached data;\n";
                        using (StreamWriter writer = new StreamWriter(logPath, true))
                        {
                            writer.WriteLine(logMessage);
                        }

                        Invoke(new Action(() =>
                        {
                            listBox1.Items.Add(logMessage);
                        }));

                }
            }
            catch (IOException ex)
            {
            }
            finally
            {
                client.Close();
            }
        }

        private void cacheStatus1_Click(object sender, EventArgs e)
        {

        }

        private void cacheStatus2_Click(object sender, EventArgs e)
        {

        }

        private void cacheStatus3_Click(object sender, EventArgs e)
        {

        }

        private void cache_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
            _tcpClient = new TcpClient(_ipAddr_cs.ToString(), _port_cs);
            // 实现删除
            string currentDirectory = Directory.GetCurrentDirectory();
            string grandParentDirectory = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", ".."));
            string dataPath = Path.Combine(grandParentDirectory, "data");
            string[] files = Directory.GetFiles(dataPath);
            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            using (NetworkStream stream_cs = _tcpClient.GetStream())
            {
                byte command = 3;
                stream_cs.WriteByte(command);
            }


                deleteStatus.Text = "Delete successfully!!!";


        }
        public static string FilterHexCharacters(string input)
        {
            var result = new StringBuilder();
            foreach (char c in input)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f'))
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
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

    }
}