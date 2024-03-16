//using System.Security.Cryptography;
//using System.Text;

//namespace StudentApi.Models
//{
//    public class EncryptDecryptManager
//    {

//        string myPassword = "TESTING!@#_123__";
//        string myPlainFile = "test.txt";
//        public static void EncryptFile(string inFile, string outFile,
//                    string password/*, CryptoProgressCallBack callback*/)
//        {
//            using (FileStream fin = File.OpenRead(inFile),
//                  fout = File.OpenWrite(outFile))
//            {
//                long lSize = fin.Length; // the size of the input file for storing
//                int size = (int)lSize;  // the size of the input file for progress
//                byte[] bytes = new byte[BUFFER_SIZE]; // the buffer
//                int read = -1; // the amount of bytes read from the input file
//                int value = 0; // the amount overall read from the input file for progress

//                // generate IV and Salt
//                byte[] IV = GenerateRandomBytes(16);
//                byte[] salt = GenerateRandomBytes(16);

//                // create the crypting object
//                SymmetricAlgorithm sma = CryptoHelp.CreateRijndael(password, salt);
//                sma.IV = IV;

//                // write the IV and salt to the beginning of the file
//                fout.Write(IV, 0, IV.Length);
//                fout.Write(salt, 0, salt.Length);

//                // create the hashing and crypto streams
//                HashAlgorithm hasher = SHA256.Create();
//                using (CryptoStream cout = new CryptoStream(fout, sma.CreateEncryptor(),
//                    CryptoStreamMode.Write),
//                      chash = new CryptoStream(Stream.Null, hasher,
//                        CryptoStreamMode.Write))
//                {
//                    // write the size of the file to the output file
//                    BinaryWriter bw = new BinaryWriter(cout);
//                    bw.Write(lSize);

//                    // write the file cryptor tag to the file
//                    bw.Write(FC_TAG);

//                    // read and the write the bytes to the crypto stream 
//                    // in BUFFER_SIZEd chunks
//                    while ((read = fin.Read(bytes, 0, bytes.Length)) != 0)
//                    {
//                        cout.Write(bytes, 0, read);
//                        chash.Write(bytes, 0, read);
//                        value += read;
//                        callback(0, size, value);
//                    }
//                    // flush and close the hashing object
//                    chash.Flush();
//                    chash.Close();

//                    // read the hash
//                    byte[] hash = hasher.Hash;

//                    // write the hash to the end of the file
//                    cout.Write(hash, 0, hash.Length);

//                    // flush and close the cryptostream
//                    cout.Flush();
//                    cout.Close();
//                }
//            }
//        }

//    }
//}
////    public class EncryptDecryptManager
////    {
////        private readonly static string key = "";
////        public static string Encrypt(string text)
////        {
////            byte[] iv = new byte[16];
////            byte[] array;
////            using (Aes aes = Aes.Create())
////            {
////                aes.Key = Encoding.UTF8.GetBytes(key);
////                aes.IV = iv;
////                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
////                using (MemoryStream ms = new MemoryStream())
////                {
////                    using (CryptoStream cryptoStream = new CryptoStream((Stream)ms, encryptor, CryptoStreamMode.Write))
////                    {
////                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
////                        {
////                            streamWriter.Write(text);

////                        }
////                        array = ms.ToArray();
////                    }
////                }
////            }
////            return Convert.ToBase64String(array);
////        }

////        public static string Decrypt(string text)
////        {
////            byte[] iv = new byte[16];
////            byte[] buffer = Convert.FromBase64String(text);

////            using (Aes aes = Aes.Create())
////            {
////                aes.Key = Encoding.UTF8.GetBytes(key);
////                aes.IV = iv;
////                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
////                using (MemoryStream ms = new MemoryStream())
////                {
////                    using (CryptoStream cryptoStream = new CryptoStream((Stream)ms, decryptor, CryptoStreamMode.Write))
////                    {
////                        using (StreamReader streamReader = new StreamReader(cryptoStream))
////                        {
////                            return streamReader.ReadToEnd();

////                        }
////                    }
////                }
////            }

////        }
////    }
////}
