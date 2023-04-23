using System.IO;
using System;
using System.Text;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string currentDirectory = Directory.GetCurrentDirectory();
            string grandParentDirectory = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", ".."));
            string fileNamepath = Path.Combine(grandParentDirectory, "file.txt");
            string[] fileContext = File.ReadAllLines(fileNamepath);
            string fileNamepath1 = Path.Combine(grandParentDirectory, "fileList");
            Console.Write(fileNamepath1)
;           List<byte[]> Robin_file = RabinFunction(fileNamepath);
            List<String> Robin_file_list = new List<String>();
            
            // 转16进制操作
            foreach (byte[] file in Robin_file) {
                Robin_file_list.Add(ByteArrayToHexString(file));
            }
            label1.Text = string.Join(Environment.NewLine, Robin_file_list);

            int fileCounter = 1;
            foreach(string data in Robin_file_list)
            {
                string fileName = $"fileList_{fileCounter}.txt";
                string filePath = Path.Combine(fileNamepath1, fileName);
                File.WriteAllText(filePath, data);
                fileCounter++;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // 转成16进制输出
        private string ByteArrayToHexString(byte[] byteArray)
        {
            StringBuilder hexString = new StringBuilder(byteArray.Length * 2);
            foreach (byte b in byteArray)
            {
                hexString.AppendFormat("{0:x2}", b);
            }
            return hexString.ToString();
        }


        // 罗宾函数
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
            foreach(byte b in code)
            {
                hash = ((hash * 17 + (int)b + 75)) %5003;
            }
            return hash % 1024;
        }


        private static List<byte[]> RabinFileChunking(string filePath, int windowSize, int avgChunkSize, int minChunkSize, int maxChunkSize)
        {
            const uint Q = 101; // large prime number
            byte[] fileData = File.ReadAllBytes(filePath);

            List<byte[]> chunks = new List<byte[]>();
            int chunkStart = 0;
            uint currentHash = 0;
            uint highPower = 1;
            uint divisor = (1 << 10) - 1; // 2^10 - 1, used to find the modulo

            // Compute the high power for the rolling hash
            for (int i = 0; i < windowSize - 1; i++)
            {
                highPower = (highPower * Q) & divisor;
            }

            // Compute the initial rolling hash
            for (int i = 0; i < windowSize; i++)
            {
                currentHash = (currentHash * Q + fileData[i]) & divisor;
            }

            for (int i = windowSize; i < fileData.Length; i++)
            {
                uint prevHash = currentHash;

                // Update the rolling hash
                currentHash = (((currentHash - fileData[i - windowSize] * highPower) * Q) + fileData[i]) & divisor;

                // Check if it's time to split the chunk
                if (((currentHash % avgChunkSize) == 0 && (i - chunkStart) >= minChunkSize) || (i - chunkStart) >= maxChunkSize)
                {
                    int chunkSize = i - chunkStart;
                    byte[] chunkData = new byte[chunkSize];
                    Buffer.BlockCopy(fileData, chunkStart, chunkData, 0, chunkSize);
                    chunks.Add(chunkData);
                    chunkStart = i;
                }
            }

            // Add the last chunk if there is any
            if (chunkStart < fileData.Length)
            {
                int chunkSize = fileData.Length - chunkStart;
                byte[] chunkData = new byte[chunkSize];
                Buffer.BlockCopy(fileData, chunkStart, chunkData, 0, chunkSize);
                chunks.Add(chunkData);
            }

            return chunks;
        }

    }
}