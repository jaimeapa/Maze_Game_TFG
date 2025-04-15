using System;
using UnityEngine;
using System.Net;
using System.Net.Mail;


using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using System.IO;

public class Report : MonoBehaviour
{
    public int time;
    public int walls;
    public int timeAtWall;
    public String maze;
    public DateTime date;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendEmail(){
        
        var fromEmail = "ampurius@gmail.com";

        var mail = new MailMessage();
        mail.From = new MailAddress(fromEmail);
        mail.To.Add("jaime.apariciohalpern@usp.ceu.es");

        mail.Subject = "Results" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm");
        mail.Body = "Results: \n" + "Walls hit: " + walls + "\nTime taken: " + time;

        var smtpServer = new SmtpClient("smtp.gmail.com"); // Gmail smtp client
        smtpServer.Port = 587; // Gmail smtp port

        smtpServer.Credentials = new System.Net.NetworkCredential(fromEmail, "12052003Jah") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { 
            return true; 
        };

        smtpServer.Send(mail);
    }
    public void WriteToFile(string filename, string data)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, filename);
        try
        {
            File.WriteAllText(fullPath, data);
            Debug.Log("Archivo guardado en: " + fullPath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al guardar el archivo: " + e.Message);
        }
    }

    public void WriteReportInFile(string filename, string date){
        string data = "Report day " + date + "\nTime taken: " + time + "\nWalls Hit: " + walls + "\n Time spent touching walls: " + timeAtWall + "\nMaze: " + maze;
        WriteToFile(filename, data);
    }
    public string ReadFromFile(string filename)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, filename);

        try
        {
            if (File.Exists(fullPath))
            {
                string data = File.ReadAllText(fullPath);
                Debug.Log("Contenido del archivo leído exitosamente: " + data);
                return data;
            }
            else
            {
                Debug.LogWarning("El archivo no existe: " + fullPath);
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al leer el archivo: " + e.Message);
            return null;
        }
    }

    public void setMaze(String maze)
    {
        this.maze = maze;
    }
    public void setWalls(int walls)
    {
        this.walls = walls; 
    }
    public void setTime(int time)
    {
        this.time = time;
    }
    public void setDate (DateTime date)
    {
        this.date = date;
    }
    public void setData(String maze, int walls, int time, int timeAtWalls)
    {
        this.maze = maze;
        this.walls = walls;
        this.time = time;
        this.timeAtWall = timeAtWalls;
    }
}
