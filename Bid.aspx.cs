using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;

namespace Lab4
{
    public partial class Bid : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList1.DataSource = DAO.GetDataTable("SELECT * FROM Members");
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "MemberId";
                DropDownList1.DataBind();

                DropDownList2.DataSource = DAO.GetDataTable("SELECT * FROM Items");
                DropDownList2.DataTextField = "ItemName";
                DropDownList2.DataValueField = "ItemID";
                DropDownList2.DataBind();

                DropDownList2.SelectedIndex = 0;
                displayItem(DropDownList2.SelectedValue.ToString());
            }
        }

        private void displayItem(string itemID)
        {
            item item = itemDAO.GetItemByID(itemID);
            TextBox1.Text = item.ItemDescription;
            Member member = MembersDAO.GetMemberByID(item.SellerID);
            TextBox7.Text = member.Name;
            TextBox2.Text = item.CurrentPrice + "";
            TextBox8.Text = item.Increment + "";
            TextBox3.Text = item.EndDate.ToString("dd/MM/yyyy, HH:mm:ss");
            TextBox4.Text = item.CurrentDate.ToString("dd/MM/yyyy, HH:mm:ss");
            TimeSpan remainTime = item.EndDate.Subtract(item.CurrentDate);
            TextBox5.Text = remainTime.ToString();
            
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string itemID = DropDownList2.SelectedValue.ToString();
            displayItem(itemID);
        }



        protected void Button2_Click(object sender, EventArgs e)
        {
            string price = TextBox6.Text;
            double bidPrice = 0;
            double currentPrice = Convert.ToDouble(TextBox2.Text);
            double increment = Convert.ToDouble(TextBox8.Text);
            string itemID = DropDownList2.SelectedValue.ToString();
            string memberId = DropDownList1.SelectedValue.ToString();

            try
            {
                bidPrice = Double.Parse(price);

                if (bidPrice < (currentPrice + increment))
                {
                    WebMsgBox.Show("Bid price must be >=" + (currentPrice + increment));
                }
                else
                {
                    DateTime d = DateTime.Now;
                    //DateTime d = Convert.ToDateTime(textBox4.Text);
                    int insertBid = BidDAO.InsertBid(itemID, memberId, d, bidPrice);
                    int updateItemPrice = itemDAO.UpdateBidPrice(itemID, bidPrice);
                    if (insertBid == 1 && updateItemPrice == 1)
                    {
                        WebMsgBox.Show("Bid item is added!");
                        displayItem(itemID);
                        TextBox6.Text = "";
                    }
                }
            }
            catch (Exception)
            {
                WebMsgBox.Show("Bid price must be a double and not empty!");
            }
        }

        class Bid1
        {
            public DateTime BidDate { get; set; }
            public double BidPrice { get; set; }
            public string Bidder { get; set; }
            public string TimeRemain { get; set; }

            public Bid1()
            {
            }

            public Bid1(DateTime bidDate, double bidPrice, string bidder, string timeRemain)
            {
                BidDate = bidDate;
                BidPrice = bidPrice;
                Bidder = bidder;
                TimeRemain = timeRemain;
            }
        }

        class BidDAO
        {
            public static int InsertBid(string itemID, string bidderID, DateTime bidDate, double bidPrice)
            {
                SqlCommand cmd = new SqlCommand("insert into Bids(ItemID, BidderID, BidDateTime, BidPrice) values (@item, @bidder, @bidDate, @bidPrice)");
                cmd.Parameters.AddWithValue("@item", itemID);
                cmd.Parameters.AddWithValue("@bidder", bidderID);
                cmd.Parameters.AddWithValue("@bidDate", bidDate);
                cmd.Parameters.AddWithValue("@bidPrice", bidPrice);
                DAO.UpdateTable(cmd);
                return 1;
            }

            public static DataTable GetBidByItemID(string itemID)
            {
                string sql = "select distinct BidDateTime, BidPrice, m.Name, (CURRENT_TIMESTAMP-BidDateTime) as TimeRemaining " +
                    "from Bids b inner join Members m on b.BidderID = m.MemberId where ItemID = " + itemID;
                return DAO.GetDataTable(sql);
            }

            public static List<Bid1> GetListBidByItemID(string itemID)
            {
                string sql = "select  b.BidDateTime, b.BidPrice, m.Name,i.EndDateTime, CURRENT_TIMESTAMP as CurrentTime " +
                    " from Bids b inner join Items i on b.ItemID = i.ItemID" +
                    " inner join Members m on b.BidderID = m.MemberId where b.ItemID = " + itemID;
                List<Bid1> lsBids = new List<Bid1>();
                DataTable dt = DAO.GetDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    Bid1 bid = new Bid1();
                    bid.BidDate = Convert.ToDateTime(dr["BidDateTime"].ToString());
                    bid.BidPrice = Convert.ToDouble(dr["BidPrice"].ToString());
                    bid.Bidder = dr["Name"].ToString();
                    DateTime endDate = Convert.ToDateTime(dr["EndDateTime"].ToString());
                    DateTime currDate = Convert.ToDateTime(dr["CurrentTime"].ToString());
                    TimeSpan remainTime = endDate.Subtract(currDate);
                    double total = Convert.ToDouble(remainTime.TotalMilliseconds.ToString());
                    if (total < 0)
                    {
                        remainTime = new TimeSpan(0, 0, 0, 0);

                    }
                    bid.TimeRemain = string.Format("{0}, {1:00}.{2:00}", (int)remainTime.Days, remainTime.Hours, remainTime.Minutes);
                    lsBids.Add(bid);
                }
                return lsBids;
            }
        }

        class MembersDAO
        {
            public static DataTable GetAllMembers()
            {
                return DAO.GetDataTable("select * from Members");
            }
            public static Member GetMemberByID(int memberId)
            {
                string sql = "select * from Members where MemberId=" + memberId;
                DataTable dt = DAO.GetDataTable(sql);
                Member member = new Member();
                foreach (DataRow dr in dt.Rows)
                {
                    member.MemberID = Convert.ToInt32(dr["MemberId"].ToString());
                    member.Name = dr["Name"].ToString();
                    member.Address = dr["Address"].ToString();
                    member.Email = dr["Email"].ToString();
                    member.Expirationdate = Convert.ToDateTime(dr["Expirationdate"].ToString());
                    member.Password = dr["Password"].ToString();
                }
                return member;
            }
        }

        class itemDAO
        {
            public static DataTable GetUnexpriedItems()
            {
                return DAO.GetDataTable("select * from Items where  EndDateTime > CURRENT_TIMESTAMP");
            }

            public static DataTable GetAllItems()
            {
                return DAO.GetDataTable("select * from Items");
            }
            public static item GetItemByID(string itemID)
            {
                string sql = "select *, CURRENT_TIMESTAMP as CurrentDate from Items where ItemID=" + itemID;
                SqlParameter sqlParameter = new SqlParameter("@ItemId", Convert.ToInt32(itemID));
                DataTable dt = DAO.GetDataTable(sql);
                item item = new item();
                foreach (DataRow dr in dt.Rows)
                {
                    item.ItemID = Convert.ToInt32(dr["ItemID"].ToString());
                    item.ItemTypeID = Convert.ToInt32(dr["ItemTypeID"].ToString());
                    item.ItemName = dr["ItemName"].ToString();
                    item.ItemDescription = dr["ItemDescription"].ToString();
                    item.SellerID = Convert.ToInt32(dr["SellerID"].ToString());
                    item.Increment = Convert.ToDouble(dr["MinimumBidIncrement"].ToString());
                    item.EndDate = Convert.ToDateTime(dr["EndDateTime"].ToString());
                    item.CurrentPrice = Convert.ToDouble(dr["CurrentPrice"].ToString());
                    item.CurrentDate = Convert.ToDateTime(dr["CurrentDate"].ToString());
                }
                return item;
            }

            public static int UpdateBidPrice(string itemID, double currentPrice)
            {
                SqlCommand cmd = new SqlCommand("update Items set CurrentPrice= @currentPrice where ItemID= @itemID");
                cmd.Parameters.AddWithValue("@currentPrice", currentPrice);
                cmd.Parameters.AddWithValue("@itemID", itemID);

                DAO.UpdateTable(cmd);
                return 1;
            }

            public static DataTable GetItemBySeller(string sellerID)
            {
                string sql = "select * from Items where SellerID=" + sellerID;
                return DAO.GetDataTable(sql);
            }
        }

        class item
        {
            public int ItemID { get; set; }
            public int ItemTypeID { get; set; }
            public string ItemName { get; set; }
            public string ItemDescription { get; set; }
            public int SellerID { get; set; }
            public double Increment { get; set; }
            public DateTime EndDate { get; set; }
            public double CurrentPrice { get; set; }
            public DateTime CurrentDate { get; set; }

            public item(int itemID, int itemTypeID, string itemName, string itemDescription, int sellerID, double increment, DateTime endDate, double currentPrice)
            {
                ItemID = itemID;
                ItemTypeID = itemTypeID;
                ItemName = itemName;
                ItemDescription = itemDescription;
                SellerID = sellerID;
                Increment = increment;
                EndDate = endDate;
                CurrentPrice = currentPrice;
            }

            public item(int itemID, int itemTypeID, string itemName, string itemDescription, int sellerID, double increment, DateTime endDate, double currentPrice, DateTime currentDate) : this(itemID, itemTypeID, itemName, itemDescription, sellerID, increment, endDate, currentPrice)
            {
                CurrentDate = currentDate;
            }

            public item()
            {
            }
        }

        class Member
        {
            public int MemberID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public DateTime Expirationdate { get; set; }
            public string Password { get; set; }

            public Member()
            {
            }

            public Member(int memberID, string name, string address, string email, DateTime expirationdate, string password)
            {
                MemberID = memberID;
                Name = name;
                Address = address;
                Email = email;
                Expirationdate = expirationdate;
                Password = password;
            }
        }


        public class WebMsgBox
        {
            protected static Hashtable handlerPages = new Hashtable();
            private WebMsgBox()
            {
            }
            public static void Show(string Message)
            {
                if (!(handlerPages.Contains(HttpContext.Current.Handler)))
                {
                    Page currentPage = (Page)HttpContext.Current.Handler;
                    if (!((currentPage == null)))
                    {
                        Queue messageQueue = new Queue();
                        messageQueue.Enqueue(Message);
                        handlerPages.Add(HttpContext.Current.Handler, messageQueue);
                        currentPage.Unload += new EventHandler(CurrentPageUnload);
                    }
                }
                else
                {
                    Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
                    queue.Enqueue(Message);
                }
            }
            private static void CurrentPageUnload(object sender, EventArgs e)
            {
                Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
                if (queue != null)
                {
                    StringBuilder builder = new StringBuilder();
                    int iMsgCount = queue.Count;
                    builder.Append("<script language='javascript'>");
                    string sMsg;
                    while ((iMsgCount > 0))
                    {
                        iMsgCount = iMsgCount - 1;
                        sMsg = System.Convert.ToString(queue.Dequeue());
                        sMsg = sMsg.Replace("\"", "'");
                        builder.Append("alert( \"" + sMsg + "\" );");
                    }
                    builder.Append("</script>");
                    handlerPages.Remove(HttpContext.Current.Handler);
                    HttpContext.Current.Response.Write(builder.ToString());
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
        }

        
    }
}