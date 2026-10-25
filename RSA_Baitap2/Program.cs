using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSA_Baitap2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== BÀI 2: MÃ HÓA BẤT ĐỐI XỨNG RSA ===");

            // 1. Sinh cặp khóa RSA 2048-bit và lưu file
            string publicKeyFile = "rsa_public.xml";
            string privateKeyFile = "rsa_private.xml";
            GenerateAndSaveKeys(publicKeyFile, privateKeyFile);

            // 2. Import khóa public & private
            string publicKeyXml = File.ReadAllText(publicKeyFile);
            string privateKeyXml = File.ReadAllText(privateKeyFile);
            Console.WriteLine("- Đã import cặp khóa RSA từ file XML thành công.");

            // 3. Mã hóa / Giải mã đoạn văn bản
            Console.Write("\nNhập một đoạn văn bản cần mã hóa: ");
            string plainText = Console.ReadLine();

            byte[] encryptedBytes = EncryptText(plainText, publicKeyXml);
            string encryptedBase64 = Convert.ToBase64String(encryptedBytes);
            Console.WriteLine("=> Kết quả mã hóa RSA (Base64): " + encryptedBase64);

            string decryptedText = DecryptText(encryptedBytes, privateKeyXml);
            Console.WriteLine("=> Kết quả giải mã RSA: " + decryptedText);

            // 4. Xử lý file dữ liệu bằng RSA
            string inputFile = "rsa_test_file.txt";
            string encryptedFile = "rsa_encrypted.dat";
            string decryptedFile = "rsa_decrypted.txt";

            if (!File.Exists(inputFile))
            {
                File.WriteAllText(inputFile, "Chao mung ban den voi bai tap ma hoa RSA trong Mon Hoc An Toan va Bao Mat Thong Tin!");
            }

            Console.WriteLine("\n[HỆ THỐNG] Đang thực hiện mã hóa và giải mã file bằng RSA...");

            Stopwatch sw = Stopwatch.StartNew();
            byte[] fileData = File.ReadAllBytes(inputFile);
            byte[] encFileData = EncryptFileBytes(fileData, publicKeyXml);
            File.WriteAllBytes(encryptedFile, encFileData);
            sw.Stop();
            Console.WriteLine($"Thời gian mã hóa file: {sw.ElapsedMilliseconds} ms");

            sw.Restart();
            byte[] decFileData = DecryptFileBytes(File.ReadAllBytes(encryptedFile), privateKeyXml);
            File.WriteAllBytes(decryptedFile, decFileData);
            sw.Stop();
            Console.WriteLine($"Thời gian giải mã file: {sw.ElapsedMilliseconds} ms");

            bool isMatch = File.ReadAllText(inputFile) == File.ReadAllText(decryptedFile);
            Console.WriteLine($"\nKết quả so sánh nội dung file: {(isMatch ? "Khớp 100%" : "Sai lệch")}");

            Console.WriteLine("\nHoàn thành Bài 2!");
        }

        static void GenerateAndSaveKeys(string pubPath, string privPath)
        {
            using (RSA rsa = RSA.Create(2048))
            {
                File.WriteAllText(pubPath, rsa.ToXmlString(false));
                File.WriteAllText(privPath, rsa.ToXmlString(true));
                Console.WriteLine("- Đã sinh cặp khóa RSA (2048-bit) và lưu ra 2 file XML.");
            }
        }

        static byte[] EncryptText(string text, string publicKeyXml)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(publicKeyXml);
                byte[] data = Encoding.UTF8.GetBytes(text);
                return rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
            }
        }

        static string DecryptText(byte[] cipherBytes, string privateKeyXml)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(privateKeyXml);
                byte[] decrypted = rsa.Decrypt(cipherBytes, RSAEncryptionPadding.OaepSHA256);
                return Encoding.UTF8.GetString(decrypted);
            }
        }

        static byte[] EncryptFileBytes(byte[] fileBytes, string publicKeyXml)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(publicKeyXml);
                return rsa.Encrypt(fileBytes, RSAEncryptionPadding.OaepSHA256);
            }
        }

        static byte[] DecryptFileBytes(byte[] encryptedBytes, string privateKeyXml)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(privateKeyXml);
                return rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA256);
            }
        }
    }
}