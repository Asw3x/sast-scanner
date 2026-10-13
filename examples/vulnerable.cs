// Заведомо уязвимый файл для проверки анализатора.
// Каждая помеченная строка должна дать находку. Не использовать как пример кода.

using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;

class Vulnerable
{
    // SAST-001: учётные данные в коде
    private const string ConnectionString = "Server=db;User Id=sa;Password=P@ssw0rd123";
    private string apiKey = "sk-live-0000000000000000";

    // SAST-002: конкатенация пользовательского ввода в запрос
    public void FindUser(SqlConnection conn, string login)
    {
        var cmd = new SqlCommand("SELECT * FROM users WHERE login = '" + login + "'", conn);
        cmd.ExecuteReader();
    }

    // SAST-003: выполнение команды ОС с неконстантным аргументом
    public void Ping(string host)
    {
        Process.Start("cmd.exe", "/c ping " + host);
    }

    // SAST-004: слабая криптография (только в режиме Deep Scan)
    public byte[] Hash(byte[] data)
    {
        using (var md5 = MD5.Create())
        {
            return md5.ComputeHash(data);
        }
    }

    // CUSTOM-001 из examples/rules.xml: захардкоженный внутренний адрес
    private string internalApi = "http://10.0.0.5:8080/api";

    // чистый код — находок быть не должно
    public int Add(int a, int b) => a + b;
}
