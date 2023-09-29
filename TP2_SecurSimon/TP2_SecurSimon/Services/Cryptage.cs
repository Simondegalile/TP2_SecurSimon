using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class Cryptage
{
    // Clé de cryptage. Elle doit être de 256 bits.
    // IMPORTANT : Pour des raisons de sécurité, il est recommandé de ne pas utiliser une clé statique et de la générer de manière sécurisée.
    private static readonly byte[] Key = new byte[32];

    // Vecteur d'initialisation. Il doit être de 128 bits.
    // IMPORTANT : Pour des raisons de sécurité, il est recommandé de ne pas utiliser un IV statique et de le générer de manière sécurisée.
    private static readonly byte[] IV = new byte[16];

    // Méthode pour chiffrer une chaîne de caractères
    public static string Encrypt(string plainText)
    {
        // Utilisation de l'algorithme AES pour le chiffrement
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key; // Attribution de la clé
            aesAlg.IV = IV;   // Attribution du vecteur d'initialisation

            // Création de l'encrypteur
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Utilisation d'un MemoryStream pour stocker les données chiffrées
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                // Utilisation d'un CryptoStream pour chiffrer les données
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    // Utilisation d'un StreamWriter pour écrire les données à chiffrer dans le CryptoStream
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }
                // Conversion des données chiffrées en Base64 pour le retour
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    // Méthode pour déchiffrer une chaîne de caractères
    public static string Decrypt(string cipherText)
    {
        // Utilisation de l'algorithme AES pour le déchiffrement
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key; // Attribution de la clé
            aesAlg.IV = IV;   // Attribution du vecteur d'initialisation

            // Création du déchiffreur
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Conversion de la chaîne chiffrée en Base64 en tableau d'octets
            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                // Utilisation d'un CryptoStream pour déchiffrer les données
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    // Utilisation d'un StreamReader pour lire les données déchiffrées du CryptoStream
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Retour des données déchiffrées
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}
