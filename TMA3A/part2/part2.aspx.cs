using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Configuration;

namespace TMA3A.part2
{
    public partial class part2 : System.Web.UI.Page
    {
        public static int curImage = 0;
        public static bool isRandomImage = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                curImage = 1;
                isRandomImage = false;
                getCaptions(curImage); //Displays the image's caption in the TextBox (imageCaption)
            }
        }

        //Used to pick a random image. All image
        //files name 1 to 20 with extension .jpg
        private int randomImage (int curImage)
        {
            Random rnd = new Random();
            curImage = rnd.Next(1, 20);
            return curImage;
        }

        //Gets the previous image. Rounds to back when reach front.
        private int preImage (int curImage)
        {
            curImage = (curImage - 1);
            if (curImage <= 0)
                return 20;
            else
                return curImage;
        }

        //Gets the next image. Rounds to the front when reach last.
        private int nextImage (int image)
        {
            image = (image + 1);
            if (image > 20)
                return 1;
            else
                return image;
        }

        /*
         * Used to query sql database.
         * @Param:
         *  sql         - the sql query with a WHERE clause
         *  id          - The parameter for the WHERE clause
         */
        private string queryDataBase (string sql, int id)
        {
            string nextSlide = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["tma3aConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int);
            cmd.Parameters["@ID"].Value = curImage;

            try
            {
                conn.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //System.Diagnostics.Debug.WriteLine(dr.GetString(0));
                    nextSlide = dr.GetString(0);

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                nextSlide = "~/part2/slideShow/1.jpg";
            }

            return nextSlide;
        }

        /*
         * Query the part2Pictures table to retrieve the captions.
         * Loads caption to page.
         * @Param:
         *  curImage        -Id of the image in the table. Range 1 to 20.
         */
        private void getCaptions (int curImage)
        {
            string sql = "SELECT imageCaption FROM part2Pictures WHERE imageId = @ID";
            string slideCaption = queryDataBase(sql, curImage);
            imageCaption.Text = slideCaption;
        }

        /*
         * Query the part2Pictures table to retrieve the location of the image on the server.
         * Loads caption to page.
         * @Param:
         *  curImage        -Id of the image in the table. Range 1 to 20
         */
        private void getImage (int curImage)
        {
            string sql = "SELECT imageLocation FROM part2Pictures WHERE imageId = @ID";
            string nextSlide = queryDataBase(sql, curImage);
            currentImage.ImageUrl = nextSlide;
        }


        /*
         * Used with Timer control. Gets next image or random image.
         */
        protected void timerPlay (object sender, EventArgs e)
        {
            if (isRandomImage)
            {
                curImage = randomImage(curImage);
            }
            else
            {
                curImage = nextImage(curImage);
            }
            getImage(curImage);
            getCaptions(curImage);
        }

        /*
         * Used with the backward button.  If random is not enable, get the previous image.
         */
        protected void backWardImage_Click(object sender, ImageClickEventArgs e)
        {
            if (isRandomImage == false)
            {
                curImage = preImage(curImage);
                getImage(curImage);
                getCaptions(curImage);
            }
        }

        /*
         * Used with the forward button.  If random is not enable, get the next image.
         */
        protected void forWardImage_Click(object sender, ImageClickEventArgs e)
        {
            if (isRandomImage == false)
            {
                curImage = nextImage(curImage);
                getImage(curImage);
                getCaptions(curImage);
            }
        }

        /*
         * Used with the play/pause button. Enables the timer for the slideshow.
         */
        protected void playPause_Click(object sender, ImageClickEventArgs e)
        {
            if (Timer1.Enabled == true)
            {
                Timer1.Enabled = false;
                playPause.ImageUrl = "~/part2/icons/play.png";
            }
            else
            {
                Timer1.Enabled = true;
                playPause.ImageUrl = "~/part2/icons/stop.png";
            } 
        }

        /*
         * Used the sequential/Random button. Sets the mode between sequential transition
         * or randomize. If randomize, disables the forward/backward buttons.
         */
        protected void sequentialRandom_Click(object sender, ImageClickEventArgs e)
        {
            if (isRandomImage == true)
            {
                isRandomImage = false;
                sequentialRandom.ImageUrl = "~/part2/icons/sequent.png";
            }
            else
            {
                isRandomImage = true;
                sequentialRandom.ImageUrl = "~/part2/icons/random.png";
            }
        }
    }
}