using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;

namespace Kemrex.Web.Main.Controllers
{
    public class ExportController : KemrexController
    {
        // GET: Export
        BaseFont f_cb = null;
        BaseFont f_cn = null;
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        ///id=QuatationId or OrderId
        public FileResult PDFQuatation(int id)
        {
            TblQuotation tblQ = uow.Modules.Quotation.Get(id);
            tblQ.TblQuotationDetail = uow.Modules.QuotationDetail.Gets(id);
            foreach (var pr in tblQ.TblQuotationDetail.ToList())
            {
                pr.Product = uow.Modules.Product.Get(pr.ProductId);
            }

                //List<TblQuotationDetail> tblDetail = uow.Modules.QuotationDetail.Gets(id);



                //f_cb = BaseFont.CreateFont(Server.MapPath("~/fonts/browa.ttf"), BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                //f_cn = BaseFont.CreateFont(Server.MapPath("~/fonts/browa.ttf"), BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                f_cb = BaseFont.CreateFont(Server.MapPath("~/fonts/browa.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, true);
            f_cn = BaseFont.CreateFont(Server.MapPath("~/fonts/browa.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, true);

            using (System.IO.MemoryStream fs = new System.IO.MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 1);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                // Add meta information to the document
                document.AddAuthor("");
                document.AddCreator("KemRex");
                document.AddKeywords("PDF Quotation");
                document.AddSubject("PDF For Print to Customer");
                document.AddTitle("PDF Quotation");

                // Open the document to enable you to write to the document
                document.Open();

                // Makes it possible to add text to a specific place in the document using 
                // a X & Y placement syntax.
                PdfContentByte cb = writer.DirectContent;
                // Add a footer template to the document
                //cb.AddTemplate(PdfFooter(cb, ""), 30, 1);
                // First we must activate writing
                cb.BeginText();


                // Add a logo to the invoice
                iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(Server.MapPath("~/images/logo-banner.png"));
                png.ScaleAbsolute(200, 55);
                png.SetAbsolutePosition(40, 750);
                cb.AddImage(png);

                // Start with the invoice type header
                writeText(cb, "ใบเสนอราคา", 350, 820, f_cb, 14);
                // HEader details; invoice number, invoice date, due date and customer Id
                writeText(cb, "เลขที่ใบ", 350, 800, f_cb, 10);
                writeText(cb, tblQ.QuotationNo, 420, 800, f_cn, 10);
                writeText(cb, "วันที่", 350, 788, f_cb, 10);
                writeText(cb, tblQ.QuotationDate.Day.ToString("00") + "/" + tblQ.QuotationDate.Month.ToString("00") + "/" + tblQ.QuotationDate.Year, 420, 788, f_cn, 10);
                writeText(cb, "วันครบกำหนด", 350, 776, f_cb, 10);
                writeText(cb, tblQ.DueDate != null? tblQ.DueDate.Value.Day.ToString("00") + "/" + tblQ.DueDate.Value.Month.ToString("00") + "/" + tblQ.DueDate.Value.Year : "", 420, 776, f_cn, 10);
                writeText(cb, "ผู้ให้บริการ", 350, 764, f_cb, 10);
                writeText(cb, "" + tblQ.SaleName, 420, 764, f_cn, 10);
                //writeText(cb, "รหัสลูกค้า", 350, 752, f_cb, 10);
                //writeText(cb, tblQ.CustomerId.Value.ToString("0000000000") ,420, 752, f_cn, 10); 


                // Delivery address details
                int left_margin = 40;
                int top_margin = 720;
                writeText(cb, "ที่อยู่ออกบิล", left_margin, top_margin, f_cb, 10);
                writeText(cb, "ชื่อลูกค้า", left_margin, top_margin - 12, f_cn, 10);
                writeText(cb, "ที่อยู่จัดส่ง", left_margin, top_margin - 24, f_cn, 10);
                writeText(cb, "ชื่อผู้ติดต่อ", left_margin, top_margin - 36, f_cn, 10);
                writeText(cb, "เบอร์โทร.", left_margin, top_margin - 48, f_cn, 10);
                writeText(cb, "Email", left_margin, top_margin - 60, f_cn, 10);
                writeText(cb, "", left_margin + 65, top_margin - 60, f_cn, 10);

                // Invoice address
                left_margin = 150;
                writeText(cb, tblQ.BillingAddress, left_margin, top_margin, f_cb, 10);
                writeText(cb, tblQ.CustomerName, left_margin, top_margin - 12, f_cn, 10);
                writeText(cb, tblQ.ShippingAddress, left_margin, top_margin - 24, f_cn, 10);
                writeText(cb, "คุณ" + tblQ.ContractName, left_margin, top_margin - 36, f_cn, 10);
                writeText(cb, tblQ.ContractPhone, left_margin, top_margin - 48, f_cn, 10);
                writeText(cb, tblQ.ContractEmail, left_margin, top_margin - 60, f_cn, 10);
                writeText(cb, "", left_margin + 65, top_margin - 60, f_cn, 10);

                // Write out invoice terms details
                //left_margin = 40;
                //top_margin = 620;
                //writeText(cb, "Payment terms", left_margin, top_margin, f_cb, 10);
                //writeText(cb, "payTerms", left_margin, top_margin - 12, f_cn, 10);
                //writeText(cb, "Delivery terms", left_margin + 200, top_margin, f_cb, 10);
                //writeText(cb, "delTerms", left_margin + 200, top_margin - 12, f_cn, 10);
                //writeText(cb, "Transport terms", left_margin + 350, top_margin, f_cb, 10);
                //writeText(cb, "delTransportTerms", left_margin + 350, top_margin - 12, f_cn, 10);
                //// Move down
                //left_margin = 40;
                //top_margin = 590;
                //writeText(cb, "Order reference", left_margin, top_margin, f_cb, 10);
                //writeText(cb, "orderReference", left_margin, top_margin - 12, f_cn, 10);
                //writeText(cb, "Customer marking", left_margin + 200, top_margin, f_cb, 10);
                //writeText(cb, "customerMarking", left_margin + 200, top_margin - 12, f_cn, 10);
                //writeText(cb, "Salesman", left_margin + 350, top_margin, f_cb, 10);
                //writeText(cb, "salesman", left_margin + 350, top_margin - 12, f_cn, 10);

                // NOTE! You need to call the EndText() method before we can write graphics to the document!
                cb.EndText();
                // Separate the header from the rows with a line
                // Draw a line by setting the line width and position
                cb.SetLineWidth(0f);
                cb.MoveTo(40, 630);
                cb.LineTo(560, 630);
                cb.Stroke();
                // Don't forget to call the BeginText() method when done doing graphics!
                cb.BeginText();

                // Before we write the lines, it's good to assign a "last position to write"
                // variable to validate against if we need to make a page break while outputting.
                // Change it to 510 to write to test a page break; the fourth line on a new page
                int lastwriteposition = 100;

                // Loop thru the rows in the rows table
                // Start by writing out the line headers
                top_margin = 600;
                left_margin = 40;
                // Line headers
                writeText(cb, "ลำดับ", left_margin, top_margin, f_cb, 10);
                writeText(cb, "รายการสินค้า", left_margin + 30, top_margin, f_cb, 10);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "ราคา/หน่วย", left_margin + 310, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "จำนวน", left_margin + 360, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "รวม", left_margin + 410, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "ส่วนลด", left_margin + 460, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "ราคาสุทธิ", left_margin + 510, top_margin, 0);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "รวม VAT(7 %)", left_margin + 500, top_margin, 0);

                // First item line position starts here
                top_margin = 585;

                // Loop thru the table of items and set the linespacing to 12 points.
                // Note that we use the -= operator, the coordinates goes from the bottom of the page!
                //for (var row = 1; row <= 5; row++)
                int row = 0;
                foreach(var ob in tblQ.TblQuotationDetail.ToList())           //tblDetail
                {
                    row++;
                    writeText(cb, row.ToString(), left_margin, top_margin, f_cn, 10);
                    writeText(cb, ob.Product.ProductName.ToString(), left_margin + 30, top_margin, f_cn, 10);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, ob.Product.PriceNet.ToString(), left_margin + 310, top_margin, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, ob.Quantity.ToString(), left_margin + 360, top_margin, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, ob.PriceNet.ToString(), left_margin + 410, top_margin, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, ob.DiscountNet.ToString(), left_margin + 460, top_margin, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, ob.TotalNet.Value.ToString(), left_margin + 510, top_margin, 0);
                    //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, ob.TotalTot.Value.ToString(), left_margin + 500, top_margin, 0);
                    // This is the line spacing, if you change the font size, you might want to change this as well.
                    top_margin -= 12;

                    // Implement a page break function, checking if the write position has reached the lastwriteposition
                    if (top_margin <= lastwriteposition)
                    {
                        // We need to end the writing before we change the page
                        cb.EndText();
                        // Make the page break
                        document.NewPage();
                        // Start the writing again
                        cb.BeginText();
                        // Assign the new write location on page two!
                        // Here you might want to implement a new header function for the new page
                        top_margin = 780;
                    }
                }

                // Okay, write out the totals table
                // Here you might want to do some page break scenarios, as well:
                // Example:
                // Calculate how many rows you are about to print and see if they fit before the lastwriteposition, 
                // then decide how to do; write some on first page, then the rest on second page or perhaps all the 
                // total lines after the page break.
                // We are not doing this here, we just write them out 80 points below the last writed item row

                top_margin -= 40;
                left_margin = 350;

                // First the headers
                writeText(cb, "ยอดรวมก่อนหักส่วนลด", left_margin, top_margin, f_cb, 10);
                writeText(cb, "รวมส่วนลด", left_margin, top_margin - 12, f_cb, 10);
                writeText(cb, "รวมทั้งสิ้น", left_margin, top_margin - 24, f_cb, 10);
                writeText(cb, "Vat 7%", left_margin, top_margin - 36, f_cb, 10);
                writeText(cb, "ยอดเงินรวม", left_margin, top_margin - 56, f_cb, 10);
                // Move right to write out the values
                left_margin = 540;
                // Write out the invoice currency and values in regular text
                cb.SetFontAndSize(f_cn, 10);
                string curr = "บาท";
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin - 12, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin - 24, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin - 36, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin - 56, 0);
                left_margin = 535;
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblQ.SubTotalNet.Value.ToString(), left_margin, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblQ.DiscountNet.Value.ToString(), left_margin, top_margin - 12, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblQ.SummaryNet.Value.ToString(), left_margin, top_margin - 24, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblQ.SummaryVat.Value.ToString(), left_margin, top_margin - 36, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblQ.SummaryTot.Value.ToString(), left_margin, top_margin - 56, 0);

                // End the writing of text
                cb.EndText();

                // Close the document, the writer and the filestream!
                document.Close();
                writer.Close();
                fs.Close();

                return File(fs.ToArray(), "application/pdf", "Quotation.pdf");
            }

        }
        private void writeText(PdfContentByte cb, string Text, int X, int Y, BaseFont font, int Size)
        {
            cb.SetFontAndSize(font, Size);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Text, X, Y, 0);
        }
        private PdfTemplate PdfFooter(PdfContentByte cb, string drFoot)
        {
            // Create the template and assign height
            PdfTemplate tmpFooter = cb.CreateTemplate(580, 70);
            // Move to the bottom left corner of the template
            tmpFooter.MoveTo(1, 1);
            // Place the footer content
            tmpFooter.Stroke();
            // Begin writing the footer
            tmpFooter.BeginText();
            // Set the font and size
            tmpFooter.SetFontAndSize(f_cn, 8);
            // Write out details from the payee table
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "supplier", 0, 53, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "address1", 0, 45, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "address2", 0, 37, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "address3", 0, 29, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Address", 0, 21, 0);
            // Bold text for ther headers
            tmpFooter.SetFontAndSize(f_cb, 8);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Phone", 215, 53, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Mail", 215, 45, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Web", 215, 37, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Legal info", 400, 53, 0);
            // Regular text for infomation fields
            tmpFooter.SetFontAndSize(f_cn, 8);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "phone", 265, 53, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "mail", 265, 45, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "web", 265, 37, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "xtrainfo", 400, 45, 0);
            // End text
            tmpFooter.EndText();
            // Stamp a line above the page footer
            cb.SetLineWidth(0f);
            cb.MoveTo(30, 60);
            cb.LineTo(570, 60);
            cb.Stroke();
            // Return the footer template
            return tmpFooter;
        }
    }
}