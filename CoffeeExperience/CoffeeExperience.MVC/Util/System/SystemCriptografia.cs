using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    public static class SystemCriptografia
    {
        #region Atributos

        private static string chave = "RASW6*RHT;X4(mgh1O_%@t6G9$LOLIu&";
        private static SymmetricAlgorithm algoritmo = new RijndaelManaged();

        #endregion

        #region Métodos

        #region Métodos privados

        private static string base64Encode(string data)
        {
            byte[] encData_byte = new byte[data.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        private static string base64Decode(string data)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();

            byte[] todecode_byte = Convert.FromBase64String(data);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        private static string Criptografa(string valor, string chave)
        {
            byte[] ByteValor = Encoding.UTF8.GetBytes(valor);

            // Seta a chave privada
            algoritmo.Mode = CipherMode.CBC;
            algoritmo.Key = Encoding.Default.GetBytes(chave);
            algoritmo.IV = Encoding.Default.GetBytes("vestconcaosimu60");

            // Interface de criptografia / Cria objeto de criptografia
            ICryptoTransform cryptoTransform = algoritmo.CreateEncryptor();

            MemoryStream _memoryStream = new MemoryStream();
            CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Write);

            // Grava os dados criptografados no MemoryStream
            _cryptoStream.Write(ByteValor, 0, ByteValor.Length);
            _cryptoStream.FlushFinalBlock();

            // Busca o tamanho dos bytes encriptados
            byte[] cryptoByte = _memoryStream.ToArray();

            // Converte para a base 64 string para uso posterior em um xml
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
        }

        private static string Descriptografa(string valor, string chave)
        {
            // Converte a base 64 string em num array de bytes
            byte[] cryptoByte = Convert.FromBase64String(valor);

            // Seta a chave privada
            algoritmo.Mode = CipherMode.CBC;
            algoritmo.Key = Encoding.Default.GetBytes(chave);
            algoritmo.IV = Encoding.Default.GetBytes("vestconcaosimu60");

            // Interface de criptografia / Cria objeto de descriptografia
            ICryptoTransform cryptoTransform = algoritmo.CreateDecryptor();

            MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
            CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);

            // Busca resultado do CryptoStream
            StreamReader _streamReader = new StreamReader(_cryptoStream);
            return _streamReader.ReadToEnd();
        }

        #endregion

        public static string ToCriptografa(this string valor)
        {
            return Criptografa(valor, chave);
        }

        public static string ToDescriptografa(this string valor)
        {
            return Descriptografa(valor, chave);
        }

        public static string ToCriptografaQueryString(this string valor)
        {
            return base64Encode(Criptografa(valor, chave));
        }

        public static string ToDescriptografaQueryString(this string valor)
        {
            return Descriptografa(base64Decode(valor), chave);
        }

        public static string ToMD5Hash(this string pInput)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pInput);
            byte[] hash = md5.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));
            return sb.ToString().ToLower();
        }

        #endregion
    }
}
