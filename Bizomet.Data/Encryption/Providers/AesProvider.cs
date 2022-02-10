using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Bizomet.Data.DataEncryption.Providers
{
	/// <summary>
	/// Implements the Advanced Encryption Standard (AES) symmetric algorithm.
	/// </summary>
	public class AesProvider : IEncryptionProvider
	{
		/// <summary>
		/// AES block size constant.
		/// </summary>
		public const int AesBlockSize = 128;

		/// <summary>
		/// Initialization vector size constant.
		/// </summary>
		public const int InitializationVectorSize = 16;

		private readonly byte[] _key;
		private readonly CipherMode _mode;
		private readonly PaddingMode _padding;
		private readonly byte[] _iv;

		/// <summary>
		/// Creates a new <see cref="AesProvider"/> instance used to perform symmetric encryption and decryption on strings.
		/// </summary>
		/// <param name="key">AES key used for the symmetric encryption.</param>
		/// <param name="mode">Mode for operation used in the symmetric encryption.</param>
		/// <param name="padding">Padding mode used in the symmetric encryption.</param>
		public AesProvider(byte[] key, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
		{
			_key = key;
			_mode = mode;
			_padding = padding;
		}

		/// <summary>
		/// Creates a new <see cref="AesProvider"/> instance used to perform symmetric encryption and decryption on strings.
		/// </summary>
		/// <param name="key">AES key used for the symmetric encryption.</param>
		/// <param name="initializationVector">AES Initialization Vector used for the symmetric encryption.</param>
		/// <param name="mode">Mode for operation used in the symmetric encryption.</param>
		/// <param name="padding">Padding mode used in the symmetric encryption.</param>
		public AesProvider(byte[] key, byte[] initializationVector, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7) : this(key, mode, padding)
		{
			// Re-enabled to allow for a static IV.
			// This reduces security, but allows for encrypted values to be searched using LINQ.
			_iv = initializationVector;
		}

		public string Encrypt(string dataToEncrypt)
		{
			if (string.IsNullOrWhiteSpace(dataToEncrypt))
				return null;

			string result;

			byte[] data = System.Text.Encoding.UTF8.GetBytes(dataToEncrypt);

			using (var aes = CreateCryptographyProvider()) {
				using (var memoryStream = new MemoryStream()) {
					byte[] initializationVector = _iv;
					if (initializationVector is null) {
						aes.GenerateIV();
						initializationVector = aes.IV;
						memoryStream.Write(initializationVector, 0, initializationVector.Length);
					}

					using (var transform = aes.CreateEncryptor(_key, initializationVector)) {
						using (var crypto = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write)) {
							crypto.Write(data, 0, data.Length);
							crypto.FlushFinalBlock();
							memoryStream.Seek(0L, SeekOrigin.Begin);
							result = StandardConverters.StreamToBase64String(memoryStream);
						}
					}
				}
			}

			return result;
		}

		public string Decrypt(string dataToDecrypt)
		{
			if (string.IsNullOrWhiteSpace(dataToDecrypt))
				return null;

			string result;

			byte[] data = Convert.FromBase64String(dataToDecrypt);

			using (var memoryStream = new MemoryStream(data)) {
				byte[] initializationVector = _iv;
				if (initializationVector is null) {
					initializationVector = new byte[InitializationVectorSize];
					memoryStream.Read(initializationVector, 0, initializationVector.Length);
				}

				using (var aes = CreateCryptographyProvider())
				using (var transform = aes.CreateDecryptor(_key, initializationVector))
				using (var crypto = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read)) {
					result = StandardConverters.StreamToString(crypto);
				}
			}

			return result;
		}

		/// <inheritdoc />
		public TStore Encrypt<TStore, TModel>(TModel dataToEncrypt, Func<TModel, byte[]> converter, Func<Stream, TStore> encoder)
		{
			if (converter is null) {
				throw new ArgumentNullException(nameof(converter));
			}

			if (encoder is null) {
				throw new ArgumentNullException(nameof(encoder));
			}

			byte[] data = converter(dataToEncrypt);
			if (data is null || data.Length == 0) {
				return default;
			}

			using var aes = CreateCryptographyProvider();
			using var memoryStream = new MemoryStream();

			byte[] initializationVector = _iv;
			if (initializationVector is null) {
				aes.GenerateIV();
				initializationVector = aes.IV;
				memoryStream.Write(initializationVector, 0, initializationVector.Length);
			}

			using var transform = aes.CreateEncryptor(_key, initializationVector);
			using var crypto = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			crypto.Write(data, 0, data.Length);
			crypto.FlushFinalBlock();

			memoryStream.Seek(0L, SeekOrigin.Begin);
			return encoder(memoryStream);
		}

		/// <inheritdoc />
		public TModel Decrypt<TStore, TModel>(TStore dataToDecrypt, Func<TStore, byte[]> decoder, Func<Stream, TModel> converter)
		{
			if (decoder is null) {
				throw new ArgumentNullException(nameof(decoder));
			}

			if (converter is null) {
				throw new ArgumentNullException(nameof(converter));
			}

			byte[] data = decoder(dataToDecrypt);
			if (data is null || data.Length == 0) {
				return default;
			}

			using var memoryStream = new MemoryStream(data);

			byte[] initializationVector = _iv;
			if (initializationVector is null) {
				initializationVector = new byte[InitializationVectorSize];
				memoryStream.Read(initializationVector, 0, initializationVector.Length);
			}

			using var aes = CreateCryptographyProvider();
			using var transform = aes.CreateDecryptor(_key, initializationVector);
			using var crypto = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
			return converter(crypto);
		}

		/// <summary>
		/// Generates an AES cryptography provider.
		/// </summary>
		/// <returns></returns>
		private AesCryptoServiceProvider CreateCryptographyProvider()
		{
			return new AesCryptoServiceProvider {
				BlockSize = AesBlockSize,
				Mode = _mode,
				Padding = _padding,
				Key = _key,
				KeySize = _key.Length * 8
			};
		}

		/// <summary>
		/// Generates an AES key.
		/// </summary>
		/// <remarks>
		/// The key size of the Aes encryption must be 128, 192 or 256 bits. 
		/// Please check https://blogs.msdn.microsoft.com/shawnfa/2006/10/09/the-differences-between-rijndael-and-aes/ for more informations.
		/// </remarks>
		/// <param name="keySize">AES Key size</param>
		/// <returns></returns>
		public static AesKeyInfo GenerateKey(AesKeySize keySize)
		{
			var crypto = new AesCryptoServiceProvider {
				KeySize = (int) keySize,
				BlockSize = AesBlockSize
			};

			crypto.GenerateKey();
			crypto.GenerateIV();

			return new AesKeyInfo(crypto.Key, crypto.IV);
		}
	}

	internal static class StandardConverters
	{
		internal static Stream BytesToStream(byte[] bytes) => new MemoryStream(bytes);

		internal static byte[] StreamToBytes(Stream stream)
		{
			if (stream is MemoryStream ms) {
				return ms.ToArray();
			}

			using (var output = new MemoryStream()) {
				stream.CopyTo(output);
				return output.ToArray();
			}
		}

		internal static string StreamToBase64String(Stream stream) => Convert.ToBase64String(StreamToBytes(stream));

		internal static string StreamToString(Stream stream)
		{
			string result;
			using (var reader = new StreamReader(stream, Encoding.UTF8)) {
				result = reader.ReadToEnd().Trim('\0');
			}

			return result;
		}

		internal static SecureString StreamToSecureString(Stream stream)
		{
			using (var reader = new StreamReader(stream, Encoding.UTF8)) {
				var result = new SecureString();
				var buffer = new char[100];
				while (!reader.EndOfStream) {
					var charsRead = reader.Read(buffer, 0, buffer.Length);
					if (charsRead != 0) {
						for (int index = 0; index < charsRead; index++) {
							char c = buffer[index];
							if (c != '\0') {
								result.AppendChar(c);
							}
						}
					}
				}

				return result;
			}
		}
	}
}
