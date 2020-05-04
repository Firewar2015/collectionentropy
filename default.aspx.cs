using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class _default : System.Web.UI.Page
{
    int nmax = 0;
    int inputn = 0;
    double res = 0;
    int i = 1;
    double fpresult = 0;
    int selectednum = 1;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        double tj = double.Parse(txt_tj.Text);
        double c = double.Parse(txt_c.Text);
        double t0 = double.Parse(txt_t0.Text);

        double re_p = result_p(tj, c, t0);
        resultP.InnerHtml = re_p.ToString();
        ///Response.Write("<br>Result：P=" + re_p);


    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        int input2 = int.Parse(Request.Form["selectnum"].ToString()) ;
        inputn = input2;
       
        if (FileUpload1.HasFile)
        {
            string filePath = Server.MapPath("~/upload/");
            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
            string fileName = filePath + FileUpload1.PostedFile.FileName;
            FileUpload1.SaveAs(fileName);
            var data = ExcelSqlConnection(fileName, "sheet1").Tables[0];
            nmax = data.Rows.Count;
            rptlist.DataSource = data;
            rptlist.ItemDataBound += Rptlist_ItemDataBound;
            rptlist.DataBind();
        }
        //// Response.Write("<br>结果：res=" + res);
        //// Response.Write("<br>结果：f(p)=" + fpresult);
        //Response.Write("<br>结果：input2=" + input2);
        resultfp.InnerHtml = fpresult.ToString();
        resultfp2.InnerHtml = (fpresult*input2).ToString();

    }

    private void Rptlist_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var drv = (DataRowView)e.Item.DataItem;
        var re = (HtmlContainerControl)e.Item.FindControl("re");

        string title = String.Format(drv[0].ToString());
        double tj = double.Parse(drv[1].ToString());
        double c = double.Parse(drv[2].ToString());
        double t0 = double.Parse(txt_t0.Text);
        re.InnerHtml = string.Format(re.InnerHtml, drv[0], drv[1], drv[2], result_p2(tj, c, t0, nmax), result_p3(tj, c, t0, nmax));

    }

    /// <summary>
    /// 公式1
    /// </summary>
    /// <param name="tj"></param>
    /// <param name="c"></param>
    /// <param name="t0"></param>
    /// <returns></returns>
    public double result_p(double tj, double c, double t0)
    {
        double re_p = (t0 - tj) / t0 - (((t0 - tj) / t0) * ((t0 - tj) / t0)) / (c + 1);
        return re_p;
    }


    /// <summary>
    /// 公式2
    /// </summary>
    /// <returns></returns>
    public double result_p2(double tj, double c, double t0, int n)
    {
        double re_pi = result_p(tj, c, 365);
        double re_p = -re_pi * Math.Log(re_pi, 2) - (1 - re_pi) * Math.Log((1 - re_pi), 2);
        //返回值
        ///res = res + i * re_p;
        ///i = i + 1;
        ///double re_p2nd = 1+ res / n;
        ///fpresult = re_p2nd;
        ///return re_p2nd;
        return re_pi;
    }

    public double result_p3(double tj, double c, double t0, int n)
    {
        double re_pi = result_p(tj, c, 365);
        double re_p = 0;
        if (re_pi > 0)
        {
            re_p = -re_pi * Math.Log(re_pi, 2) - (1 - re_pi) * Math.Log((1 - re_pi), 2);
        }
        else
        {
            re_p = 0;
        }
        


        //返回值
        res = res + i * re_p;
        i = i + 1;
        double re_p2nd = 1 + res / n;
        fpresult = re_p2nd;
        ///return re_p2nd;
        return res;
    }



    #region 连接Excel  读取Excel数据   并返回DataSet数据集合
    /// <summary>
    /// 连接Excel  读取Excel数据   并返回DataSet数据集合
    /// </summary>
    /// <param name="filepath">Excel服务器路径</param>
    /// <param name="tableName">Excel表名称</param>
    /// <returns></returns>
    public static System.Data.DataSet ExcelSqlConnection(string filepath, string tableName)
    {
        string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
        string connstr2007 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
        OleDbConnection ExcelConn = new OleDbConnection(strCon);
        string strCom = string.Format("SELECT * FROM [" + tableName + "$]");
        try
        {
            ExcelConn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, ExcelConn);
            DataSet ds = new DataSet();
            myCommand.Fill(ds, "[" + tableName + "$]");
            ExcelConn.Close();
            return ds;
        }
        catch (Exception ex)
        {
            ExcelConn.Close();
            try
            {

                ExcelConn = new OleDbConnection(connstr2007);
                ExcelConn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, ExcelConn);
                DataSet ds = new DataSet();
                myCommand.Fill(ds, "[" + tableName + "$]");
                ExcelConn.Close();
                return ds;
            }
            catch (Exception ex1)
            {
                ExcelConn.Close();
                throw;
                return null;
            }
        }
    }
    #endregion
}
