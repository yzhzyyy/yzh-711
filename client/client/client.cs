using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client
{
    public partial class Client : Form
    {
        private TcpClient _client; // 定义客户端TCP
        private string _selectedFile;  // 定义选中文件名的索引
        IPAddress _ipAddr = IPAddress.Loopback;
        int _port_cc = 8082;

        public Client()
        {
            InitializeComponent();
            clientStatus1.Text = "Start Client";
            try
            {
                clientStatus.Text = "cache connected";
            }
            catch (Exception ex)
            {
                clientStatus.Text = ex.Message;
            }
        }
        private void clientStatus_Click(object sender, EventArgs e)
        {

        }

        private void clientStatus1_Click(object sender, EventArgs e)
        {

        }

        private void showFile_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                _client = new TcpClient(_ipAddr.ToString(), _port_cc);
                byte command = 0;
                using (NetworkStream stream_cc = _client.GetStream())
                {
                    stream_cc.WriteByte(command);
                    stream_cc.Flush();

                    StreamReader reader_cc = new StreamReader(stream_cc, Encoding.UTF8);

                    string filelist_str = reader_cc.ReadToEnd();
                    List<string> filelist = filelist_str.Split(", ").ToList();
                    listBox1.Items.AddRange(filelist.ToArray());

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // _selectedIndex= listBox1.SelectedIndex; // 获取选中文件名的索引
            downLoad.Enabled = listBox1.SelectedItem != null;
            _selectedFile = listBox1.SelectedItem as string ?? string.Empty; // 选中的文件名
            downloadFile.Text = _selectedFile;// 显示选中的文件名
        }


        private void downLoad_Click(object sender, EventArgs e)
        {
            check.Text = "Downloading...";
            if (listBox1.SelectedItems != null)
            {
                byte command = 1;

                // 将文件名转换成比特的形式
                byte[] fileNameBytes = Encoding.UTF8.GetBytes(_selectedFile);

                // 存储转换成比特的文件名的长度
                byte[] fileNameLengthBytes = BitConverter.GetBytes(fileNameBytes.Length);

                // 创建比特流
                byte[] data = new byte[5 + fileNameBytes.Length];

                // 第0位存储command
                data[0] = command;

                // 将fileNameLengthBytes从第0位开始复制到data的第1位往后
                Array.Copy(fileNameLengthBytes, 0, data, 1, 4);
                Array.Copy(fileNameBytes, 0, data, 5, fileNameBytes.Length);

                IPAddress ipAddr = IPAddress.Loopback;
                int port_cc = 8082;
                _client = new TcpClient(ipAddr.ToString(), port_cc);

                // 判断client是否与cache链接
                if (!_client.Connected)
                {
                    check.Text = "Client is not connected to the cache";
                    return;
                }


                using (NetworkStream stream_cc = _client.GetStream())
                {
                    // 将包含文件名的流传给cache
                    stream_cc.Write(data, 0, data.Length);
                    stream_cc.Flush();

                    byte[] data1 = new byte[4]; // 存一共传回来多少个块
                    stream_cc.Read(data1, 0, 4);
                    int blockNum = BitConverter.ToInt32(data1);
                    string finalResult = "";
                    for (int i = 0; i < blockNum; i++)
                    {
                        byte[] data2 = new byte[4]; // 存block的长度
                        stream_cc.Read(data2, 0, 4);
                        int blockSize = BitConverter.ToInt32(data2);

                        // 存block信息
                        byte[] data3 = new byte[blockSize];
                        stream_cc.Read(data3, 0, blockSize);


                        // 在fileContext输出
                        finalResult += Encoding.UTF8.GetString(data3);
                    }

                    string result = HexStringToString(finalResult);

                    filecontext.Text = result;

                    

                    stream_cc.Close();
                    _client.Close();

                }

            }

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void downloadFile_Click(object sender, EventArgs e)
        {

        }

        private void Client_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            check.Text = "Downloading...";
            if (listBox1.SelectedItems != null)
            {
                byte command = 1;

                // 将文件名转换成比特的形式
                byte[] fileNameBytes = Encoding.UTF8.GetBytes(_selectedFile);

                // 存储转换成比特的文件名的长度
                byte[] fileNameLengthBytes = BitConverter.GetBytes(fileNameBytes.Length);

                // 创建比特流
                byte[] data = new byte[5 + fileNameBytes.Length];

                // 第0位存储command
                data[0] = command;

                // 将fileNameLengthBytes从第0位开始复制到data的第1位往后
                Array.Copy(fileNameLengthBytes, 0, data, 1, 4);
                Array.Copy(fileNameBytes, 0, data, 5, fileNameBytes.Length);

                IPAddress ipAddr = IPAddress.Loopback;
                int port_cc = 8082;
                _client = new TcpClient(ipAddr.ToString(), port_cc);

                // 判断client是否与cache链接
                if (!_client.Connected)
                {
                    check.Text = "Client is not connected to the cache";
                    return;
                }


                using (NetworkStream stream_cc = _client.GetStream())
                {
                    // 将包含文件名的流传给cache
                    stream_cc.Write(data, 0, data.Length);
                    stream_cc.Flush();

                    byte[] data1 = new byte[4]; // 存block的长度
                    stream_cc.Read(data1, 0, 4);
                    int blockNum = BitConverter.ToInt32(data1);
                    check.Text = blockNum.ToString();
                    //List<byte> finalBytesList = new List<byte> ();
                    string finalResult = "";

                    for (int i = 0; i < blockNum; i++)
                    {
                        byte[] data2 = new byte[4]; // 存block的长度
                        stream_cc.Read(data2, 0, 4);
                        int blockSize = BitConverter.ToInt32(data2);

                        // 存block信息
                        byte[] data3 = new byte[blockSize];
                        stream_cc.Read(data3, 0, blockSize);
                        finalResult = finalResult + Encoding.UTF8.GetString(data3);
                        // 将data3字节添加到finalBytesList中
                        // finalBytesList.AddRange(data3);
                    }

                    byte[] imageBytes = HexStringToByteArray(finalResult);
                    Image image = ByteArrayToImage(imageBytes);
                    Invoke((Action)(() => pictureBox1.Image = image));

                    stream_cc.Close();
                    _client.Close();

                }
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
        public byte[] HexStringToByteArray(string hexString)
        {
            hexString = FilterHexCharacters(hexString);
            int length = hexString.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return bytes;
        }
       
        public Image HexStringToImage(string hexString)
        {
            // 将十六进制字符串转换为字节数组
            string filteredHexString = FilterHexCharacters(hexString);
            byte[] imageBytes = HexStringToByteArray(filteredHexString);

            // 使用字节数组创建内存流
            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
            {
                // 从内存流中加载图片
                Image image = Image.FromStream(memoryStream);
                return image;
            }
        }

        public Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(memoryStream);
                return image;
            }
        }
        public string HexStringToString(string hex)
        {
            hex = FilterHexCharacters(hex);
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];

            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return Encoding.UTF8.GetString(bytes);
        }

    }
}