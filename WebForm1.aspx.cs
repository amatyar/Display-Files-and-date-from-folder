﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] filePaths = Directory.GetFiles(@"C:\Users\Rabindra\OneDrive\Documents");// choose location

                List<ListItem> files = new List<ListItem>();
              
                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }
                GridView1.DataSource = files;
                GridView1.DataBind();
            }
        }
        protected void UploadFile(object sender, EventArgs e)
        {
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            if (FileUpload1.HasFile)
                try
                {
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/folder") + fileName);
                    Label1.Text = "File Uploaded Sucessfully !! " + FileUpload1.PostedFile.ContentLength + "mb";
                }
                catch (Exception ex)
                {
                    Label1.Text = "File Not Uploaded!!" + ex.Message.ToString(); 
                }
            else
            {
                Label1.Text = "Please Select File and Upload Again"; 
            }


            

        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}