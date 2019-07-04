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
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;

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
                Document document = new Document(PageSize.A4, 25, 25, 40, 1);
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
                int top_margin = 800;
                // Start with the invoice type header
                writeText(cb, "ใบเสนอราคา", 350, 820, f_cb, 14);
                // HEader details; invoice number, invoice date, due date and customer Id
                writeText(cb, "เลขที่ใบ", 350, top_margin, f_cb, 12);
                writeText(cb, tblQ.QuotationNo, 420, top_margin, f_cn, 12);
                writeText(cb, "วันที่", 350, top_margin - 20, f_cb, 12);
                writeText(cb, tblQ.QuotationDate.Day.ToString("00") + "/" + tblQ.QuotationDate.Month.ToString("00") + "/" + tblQ.QuotationDate.Year, 420, top_margin - 20, f_cn, 12);
                writeText(cb, "วันครบกำหนด", 350, top_margin - 40, f_cb, 12);
                writeText(cb, tblQ.DueDate != null? tblQ.DueDate.Value.Day.ToString("00") + "/" + tblQ.DueDate.Value.Month.ToString("00") + "/" + tblQ.DueDate.Value.Year : "", 420, top_margin - 40, f_cn, 12);
                writeText(cb, "ผู้ให้บริการ", 350, top_margin - 60, f_cb, 12);
                writeText(cb, "" + tblQ.SaleName, 420, top_margin - 60, f_cn, 12);
                //writeText(cb, "รหัสลูกค้า", 350, 752, f_cb, 12);
                //writeText(cb, tblQ.CustomerId.Value.ToString("0000000000") ,420, 752, f_cn, 12); 


                // Delivery address details
                int left_margin = 40;
                top_margin = 720;
                writeText(cb, "ที่อยู่ออกบิล", left_margin, top_margin, f_cb, 12);
                writeText(cb, "ชื่อลูกค้า", left_margin, top_margin - 20, f_cn, 12);
                writeText(cb, "ที่อยู่จัดส่ง", left_margin, top_margin - 40, f_cn, 12);
                writeText(cb, "ชื่อผู้ติดต่อ", left_margin, top_margin - 60, f_cn, 12);
                writeText(cb, "เบอร์โทร.", left_margin, top_margin - 80, f_cn, 12);
                writeText(cb, "Email", left_margin, top_margin - 100, f_cn, 12);
                writeText(cb, "", left_margin + 65, top_margin - 120, f_cn, 12);

                // Invoice address
                left_margin = 150;
                writeText(cb, tblQ.BillingAddress, left_margin, top_margin, f_cb, 12);
                writeText(cb, tblQ.CustomerName, left_margin, top_margin - 20, f_cn, 12);
                writeText(cb, tblQ.ShippingAddress, left_margin, top_margin - 40, f_cn, 12);
                writeText(cb, "คุณ" + tblQ.ContractName, left_margin, top_margin - 60, f_cn, 12);
                writeText(cb, tblQ.ContractPhone, left_margin, top_margin - 80, f_cn, 12);
                writeText(cb, tblQ.ContractEmail, left_margin, top_margin - 100, f_cn, 12);
                writeText(cb, "", left_margin + 65, top_margin - 120, f_cn, 90);

                // Write out invoice terms details
                //left_margin = 40;
                //top_margin = 620;
                //writeText(cb, "Payment terms", left_margin, top_margin, f_cb, 12);
                //writeText(cb, "payTerms", left_margin, top_margin - 12, f_cn, 12);
                //writeText(cb, "Delivery terms", left_margin + 200, top_margin, f_cb, 12);
                //writeText(cb, "delTerms", left_margin + 200, top_margin - 12, f_cn, 12);
                //writeText(cb, "Transport terms", left_margin + 350, top_margin, f_cb, 12);
                //writeText(cb, "delTransportTerms", left_margin + 350, top_margin - 12, f_cn, 12);
                //// Move down
                //left_margin = 40;
                //top_margin = 590;
                //writeText(cb, "Order reference", left_margin, top_margin, f_cb, 12);
                //writeText(cb, "orderReference", left_margin, top_margin - 12, f_cn, 12);
                //writeText(cb, "Customer marking", left_margin + 200, top_margin, f_cb, 12);
                //writeText(cb, "customerMarking", left_margin + 200, top_margin - 12, f_cn, 12);
                //writeText(cb, "Salesman", left_margin + 350, top_margin, f_cb, 12);
                //writeText(cb, "salesman", left_margin + 350, top_margin - 12, f_cn, 12);

                // NOTE! You need to call the EndText() method before we can write graphics to the document!
                cb.EndText();
                // Separate the header from the rows with a line
                // Draw a line by setting the line width and position
                cb.SetLineWidth(0f);
                cb.MoveTo(40, 600);
                cb.LineTo(560, 600);
                cb.Stroke();
                // Don't forget to call the BeginText() method when done doing graphics!
                cb.BeginText();

                // Before we write the lines, it's good to assign a "last position to write"
                // variable to validate against if we need to make a page break while outputting.
                // Change it to 510 to write to test a page break; the fourth line on a new page
                int lastwriteposition = 100;

                // Loop thru the rows in the rows table
                // Start by writing out the line headers
                top_margin = 580;
                left_margin = 40;
                // Line headers
                writeText(cb, "ลำดับ", left_margin, top_margin, f_cb, 12);
                writeText(cb, "รายการสินค้า", left_margin + 30, top_margin, f_cb, 12);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "ราคา/หน่วย", left_margin + 310, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "จำนวน", left_margin + 360, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "รวม", left_margin + 410, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "ส่วนลด", left_margin + 460, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "ราคาสุทธิ", left_margin + 510, top_margin, 0);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "รวม VAT(7 %)", left_margin + 500, top_margin, 0);

                // First item line position starts here
                top_margin = 560;

                // Loop thru the table of items and set the linespacing to 12 points.
                // Note that we use the -= operator, the coordinates goes from the bottom of the page!
                //for (var row = 1; row <= 5; row++)
                int row = 0;
                foreach(var ob in tblQ.TblQuotationDetail.ToList())           //tblDetail
                {
                    row++;
                    writeText(cb, row.ToString(), left_margin, top_margin, f_cn, 12);
                    writeText(cb, ob.Product.ProductName.ToString(), left_margin + 30, top_margin, f_cn, 12);
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
                writeText(cb, "ยอดรวมก่อนหักส่วนลด", left_margin, top_margin, f_cb, 12);
                writeText(cb, "รวมส่วนลด", left_margin, top_margin - 15, f_cb, 12);
                writeText(cb, "รวมทั้งสิ้น", left_margin, top_margin - 30, f_cb, 12);
                writeText(cb, "Vat 7%", left_margin, top_margin - 45, f_cb, 12);
                writeText(cb, "ยอดเงินรวม", left_margin, top_margin - 65, f_cb, 12);
                // Move right to write out the values
                left_margin = 540;
                // Write out the invoice currency and values in regular text
                cb.SetFontAndSize(f_cn, 12);
                string curr = "บาท";
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin - 15, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin - 30, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin - 45, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin - 65, 0);
                left_margin = 535;
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblQ.SubTotalNet.Value.ToString(), left_margin, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblQ.DiscountNet.Value.ToString(), left_margin, top_margin - 15, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblQ.SummaryNet.Value.ToString(), left_margin, top_margin - 30, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblQ.SummaryVat.Value.ToString(), left_margin, top_margin - 45, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblQ.SummaryTot.Value.ToString(), left_margin, top_margin - 65, 0);

                // End the writing of text
                cb.EndText();

                // Close the document, the writer and the filestream!
                document.Close();
                writer.Close();
                fs.Close();

                return File(fs.ToArray(), "application/pdf", "Quotation.pdf");
            }

        }
        public FileResult PDFSaleOrder(int id)
        {
            TblSaleOrder tblS = uow.Modules.SaleOrder.Get(id);
            tblS.TblSaleOrderDetail = uow.Modules.SaleOrderDetail.Gets(id);
            foreach (var pr in tblS.TblSaleOrderDetail.ToList())
            {
                pr.Product = uow.Modules.Product.Get(pr.ProductId);
            }
            EnmPaymentCondition epc = new EnmPaymentCondition();
            epc = uow.Modules.PaymentCondition.Get(tblS.ConditionId.Value);
            //List<TblQuotationDetail> tblDetail = uow.Modules.QuotationDetail.Gets(id);



            //f_cb = BaseFont.CreateFont(Server.MapPath("~/fonts/browa.ttf"), BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //f_cn = BaseFont.CreateFont(Server.MapPath("~/fonts/browa.ttf"), BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            f_cb = BaseFont.CreateFont(Server.MapPath("~/fonts/browa.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, true);
            f_cn = BaseFont.CreateFont(Server.MapPath("~/fonts/browa.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, true);

            using (System.IO.MemoryStream fs = new System.IO.MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 40, 1);
           
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                // Add meta information to the document
                document.AddAuthor("");
                document.AddCreator("KemRex");
                document.AddKeywords("PDF SaleOrder");
                document.AddSubject("PDF For Print to Customer");
                document.AddTitle("PDF SaleOrder");

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
                int top_margin = 820;
                // Start with the invoice type header
                writeText(cb, "Sale Order", 350, top_margin, f_cb, 16);
                // HEader details; invoice number, invoice date, due date and customer Id
                writeText(cb, "เลขที่ใบสั่งขาย", 350, top_margin - 20, f_cb, 12);
                writeText(cb, tblS.SaleOrderNo, 430, top_margin - 20, f_cn, 12);
                writeText(cb, "วันที่สั่งขาย", 350, top_margin - 40, f_cb, 12);
                writeText(cb, tblS.SaleOrderDate.Value.Day.ToString("00") + "/" + tblS.SaleOrderDate.Value.Month.ToString("00") + "/" + tblS.SaleOrderDate.Value.Year, 430, top_margin - 40, f_cn, 12);
                writeText(cb, "จำนวนวันเครดิต", 350, top_margin - 60, f_cb, 12);
                writeText(cb, tblS.SaleOrderCreditDay == 0? "":tblS.SaleOrderCreditDay.ToString(), 430, top_margin - 60, f_cn, 12);
                writeText(cb, "วันที่กำหนดส่ง", 350, top_margin - 80, f_cb, 12);
                writeText(cb, tblS.DeliveryDateToString == null ? "": tblS.DeliveryDateToString, 430, top_margin - 80, f_cn, 12);
                writeText(cb, "เงื่อนไขการชำระเงิน", 350, top_margin - 100, f_cb, 12);
                writeText(cb, epc.ConditionName, 430, top_margin - 100, f_cn, 12);

                //writeText(cb, "รหัสลูกค้า", 350, 752, f_cb, 12);
                //writeText(cb, tblQ.CustomerId.Value.ToString("0000000000") ,420, 752, f_cn, 12); 


                // Delivery address details
                int left_margin = 40;
                top_margin = 690;
                writeText(cb, "ที่อยู่ออกบิล", left_margin, top_margin, f_cb, 12);
                writeText(cb, "ชื่อลูกค้า", left_margin, top_margin - 20, f_cn, 12);
                writeText(cb, "ที่อยู่จัดส่ง", left_margin, top_margin - 40, f_cn, 12);
                writeText(cb, "ชื่อผู้ติดต่อ", left_margin, top_margin - 60, f_cn, 12);
                //writeText(cb, "เบอร์โทร.", left_margin, top_margin - 48, f_cn, 12);
                //writeText(cb, "Email", left_margin, top_margin - 60, f_cn, 12);
                //writeText(cb, "", left_margin + 65, top_margin - 60, f_cn, 12);

                // Invoice address
                left_margin = 150;
                writeText(cb, tblS.BillingAddress, left_margin, top_margin, f_cb, 12);
                writeText(cb, tblS.CustomerName, left_margin, top_margin - 20, f_cn, 12);
                writeText(cb, tblS.ShippingAddress, left_margin, top_margin - 40, f_cn, 12);
                writeText(cb, "คุณ" + tblS.ContractName, left_margin, top_margin - 60, f_cn, 12);
                //writeText(cb, tblS.ContractPhone, left_margin, top_margin - 48, f_cn, 12);
                //writeText(cb, tblS.ContractEmail, left_margin, top_margin - 60, f_cn, 12);
                //writeText(cb, "", left_margin + 65, top_margin - 60, f_cn, 12);

                // Write out invoice terms details
                //left_margin = 40;
                //top_margin = 620;
                //writeText(cb, "Payment terms", left_margin, top_margin, f_cb, 12);
                //writeText(cb, "payTerms", left_margin, top_margin - 12, f_cn, 12);
                //writeText(cb, "Delivery terms", left_margin + 200, top_margin, f_cb, 12);
                //writeText(cb, "delTerms", left_margin + 200, top_margin - 12, f_cn, 12);
                //writeText(cb, "Transport terms", left_margin + 350, top_margin, f_cb, 12);
                //writeText(cb, "delTransportTerms", left_margin + 350, top_margin - 12, f_cn, 12);
                //// Move down
                //left_margin = 40;
                //top_margin = 590;
                //writeText(cb, "Order reference", left_margin, top_margin, f_cb, 12);
                //writeText(cb, "orderReference", left_margin, top_margin - 12, f_cn, 12);
                //writeText(cb, "Customer marking", left_margin + 200, top_margin, f_cb, 12);
                //writeText(cb, "customerMarking", left_margin + 200, top_margin - 12, f_cn, 12);
                //writeText(cb, "Salesman", left_margin + 350, top_margin, f_cb, 12);
                //writeText(cb, "salesman", left_margin + 350, top_margin - 12, f_cn, 12);

                // NOTE! You need to call the EndText() method before we can write graphics to the document!
                cb.EndText();
                // Separate the header from the rows with a line
                // Draw a line by setting the line width and position
                cb.SetLineWidth(0f);
                cb.MoveTo(40, 610);
                cb.LineTo(560, 610);
                cb.Stroke();
                // Don't forget to call the BeginText() method when done doing graphics!
                cb.BeginText();

                // Before we write the lines, it's good to assign a "last position to write"
                // variable to validate against if we need to make a page break while outputting.
                // Change it to 510 to write to test a page break; the fourth line on a new page
                int lastwriteposition = 100;

                // Loop thru the rows in the rows table
                // Start by writing out the line headers
                top_margin = 580;
                left_margin = 40;
                // Line headers
                writeText(cb, "ลำดับ", left_margin, top_margin, f_cb, 12);
                writeText(cb, "รายการสินค้า", left_margin + 30, top_margin, f_cb, 12);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "ราคา/หน่วย", left_margin + 310, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "จำนวน", left_margin + 360, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "รวม", left_margin + 410, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "ส่วนลด", left_margin + 460, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "ราคาสุทธิ", left_margin + 510, top_margin, 0);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "รวม VAT(7 %)", left_margin + 500, top_margin, 0);

                // First item line position starts here
                top_margin = 565;

                // Loop thru the table of items and set the linespacing to 12 points.
                // Note that we use the -= operator, the coordinates goes from the bottom of the page!
                //for (var row = 1; row <= 5; row++)
                int row = 0;
                foreach (var ob in tblS.TblSaleOrderDetail.ToList())           //tblDetail
                {
                    row++;
                    writeText(cb, row.ToString(), left_margin, top_margin, f_cn, 12);
                    writeText(cb, ob.Product.ProductName.ToString(), left_margin + 30, top_margin, f_cn, 12);
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
                writeText(cb, "ยอดรวมก่อนหักส่วนลด", left_margin, top_margin, f_cb, 12);
                writeText(cb, "รวมส่วนลด", left_margin, top_margin - 15, f_cb, 12);
                writeText(cb, "รวมทั้งสิ้น", left_margin, top_margin - 30, f_cb, 12);
                writeText(cb, "Vat 7%", left_margin, top_margin - 45, f_cb, 12);
                writeText(cb, "ยอดเงินรวม", left_margin, top_margin - 65, f_cb, 12);
                // Move right to write out the values
                left_margin = 540;
                // Write out the invoice currency and values in regular text
                cb.SetFontAndSize(f_cn, 12);
                string curr = "บาท";
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin - 15, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin - 30, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin - 45, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, curr, left_margin, top_margin - 65, 0);
                left_margin = 535;
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblS.SubTotalNet.Value.ToString(), left_margin, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblS.DiscountNet.Value.ToString(), left_margin, top_margin - 15, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblS.SummaryNet.Value.ToString(), left_margin, top_margin - 30, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblS.SummaryVat.Value.ToString(), left_margin, top_margin - 45, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, tblS.SummaryTot.Value.ToString(), left_margin, top_margin - 65, 0);



                left_margin = 130;
                top_margin -= 380;
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "_________________________", left_margin, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(       " + tblS.SaleName + "      )", left_margin, top_margin - 15, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "พนักงานขาย", left_margin, top_margin - 30, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "วันที่                              ", left_margin, top_margin - 45, 0);

                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "_________________________", left_margin + 180, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(                                          )", left_margin + 180, top_margin - 15, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "ผู้จัดการฝ่ายขาย", left_margin + 180, top_margin - 30, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "วันที่                             ", left_margin + 180, top_margin - 45, 0);

                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "_________________________", left_margin + 360, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(                                          )", left_margin + 360, top_margin - 15, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "พนักงานบัญชี", left_margin + 360, top_margin - 30, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "วันที่                              ", left_margin + 360, top_margin - 45, 0);

                
                // End the writing of text
                cb.EndText();

                cb.MoveTo(30, 60);
                cb.LineTo(570, 60);
                cb.Stroke();
                // Close the document, the writer and the filestream!
                document.Close();
                writer.Close();
                fs.Close();

                return File(fs.ToArray(), "application/pdf", "SaleOrder.pdf");
            }

        }
        
        public FileResult PDFJobOrder(int id)
        {
            TblJobOrder tbl = uow.Modules.JobOrder.Get(id);
            tbl.SaleOrder = uow.Modules.SaleOrder.Get(tbl.SaleOrderId.HasValue ? tbl.SaleOrderId.Value : -1);
            tbl.SaleOrder.Sale = uow.Modules.Employee.Get(tbl.SaleOrder.SaleId.HasValue ? tbl.SaleOrder.SaleId.Value : -1);
            DataTable dtCate = new DataTable();
            dtCate.Columns.Add("CategoryID", typeof(int));
            dtCate.Columns.Add("CategoryTypID", typeof(int));
            dtCate.Columns.Add("CategoryName", typeof(String));
            foreach (var ptype in tbl.ProjectType.ToList())
            {
                DataRow row = dtCate.NewRow();
                row["CategoryID"] = uow.Modules.SysCategory.Get(ptype.ProjectTypeId).CategoryId;
                row["CategoryTypID"] = uow.Modules.SysCategory.Get(ptype.ProjectTypeId).CategoryTypeId;
                row["CategoryName"] = uow.Modules.SysCategory.Get(ptype.ProjectTypeId).CategoryName;
                dtCate.Rows.Add(row);
            }
            TblProduct prd = uow.Modules.Product.Get(tbl.ProductId.HasValue ? tbl.ProductId.Value : -1);

            String html = string.Empty;
            html = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/html/" + "JobOrderHTML.html"));
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                html = html.Replace("@@ImageBanner@@", HttpContext.Server.MapPath("~/images/logo-banner.png"));
                html = html.Replace("@@ImageCheckbox@@", HttpContext.Server.MapPath("~/html/img/checkbox_0.gif"));

                html = html.Replace("@@JobName@@", tbl.JobName);
                html = html.Replace("@@JobOrderNo@@", tbl.JobOrderNo);
                string txtStartDate = tbl.StartDate == null ? "_______________" : tbl.StartDate.Value.Day.ToString("00") + "/" + tbl.StartDate.Value.Month.ToString("00") + "/" + tbl.StartDate.Value.Year;
                string txtEndtDate = tbl.EndDate == null ? "_______________" : tbl.EndDate.Value.Day.ToString("00") + "/" + tbl.EndDate.Value.Month.ToString("00") + "/" + tbl.EndDate.Value.Year;
                html = html.Replace("@@StartDate@@", txtStartDate);
                html = html.Replace("@@EndDate@@", txtEndtDate);
                html = html.Replace("@@StartWorkingTime@@", tbl.StartWorkingTime); 
                html = html.Replace("@@EndWorkingTime@@", tbl.EndWorkingTime);
                html = html.Replace("@@ProjectName@@", tbl.ProjectName);
                html = html.Replace("@@SaleName@@", tbl.SaleOrder.SaleName);
                html = html.Replace("@@SaleMobile@@", tbl.SaleOrder.Sale.EmpMobile);
                html = html.Replace("@@CustomerName@@", tbl.CustomerName);
                html = html.Replace("@@CustomerPhone@@", tbl.CustomerPhone);
                html = html.Replace("@@CustomerEmail@@", tbl.CustomerEmail);

                int rowcheck = 0;
                string CategoryName = ""; 
                foreach (DataRow dr in dtCate.Rows)
                {
                    rowcheck++;
                    if (rowcheck <= 3)
                    {
                        CategoryName += dr["CategoryName"].ToString() + "&nbsp; &nbsp; &nbsp;";
                    }
                }
                html = html.Replace("@@CategoryName@@", CategoryName); 
                html = html.Replace("@@ProductSaftyFactory@@", tbl.ProductSaftyFactory == null?"_____": tbl.ProductSaftyFactory.ToString());
                html = html.Replace("@@ProductModel@@", prd.ProductCode + " " + prd.ProductName);
                html = html.Replace("@@ProductQuantity@@", tbl.ProductQty.HasValue ? tbl.ProductQty.Value.ToString() : "_________");

                html = html.Replace("@@ProductWeight@@", tbl.ProductWeight.HasValue ? tbl.ProductWeight.Value.ToString() : "_________________");
                html = html.Replace("@@Adapter@@", tbl.Adapter);
                html = html.Replace("@@HouseNo@@", tbl.HouseNo);
                html = html.Replace("@@VillageNo@@", tbl.VillageNo);

                html = html.Replace("@@District@@", "____________________________________________________________");



                StringReader sr = new StringReader(html);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "JobOrder_"+ tbl.JobOrderNo + ".pdf");
            }
        }
        public FileResult PDFTransferOut(int id)
        {
            TransferHeader transferH = uow.Modules.Transfer.Get(id);

            String html = string.Empty;
            html = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/html/" + "TransferOutHTML.html"));
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                html = html.Replace("@@ImageBanner@@", HttpContext.Server.MapPath("~/images/logo-banner.png"));
                html = html.Replace("@@ImageCheckbox@@", HttpContext.Server.MapPath("~/html/img/checkbox_0.gif"));

                html = html.Replace("@@TransferNo@@", transferH.TransferNo);
                html = html.Replace("@@Goto@@", transferH.ReceiveTo);
                string txtTransferDate = transferH.TransferDate == null ? "__________" : transferH.TransferDate.Value.Day.ToString("00") + "/" + transferH.TransferDate.Value.Month.ToString("00") + "/" + transferH.TransferDate.Value.Year;
                string txtTransferTime = transferH.TransferTime;

                html = html.Replace("@@TransferDateTime@@", txtTransferDate + " " + txtTransferTime);
                html = html.Replace("@@For@@", transferH.Reason);
                html = html.Replace("@@PersonName@@", transferH.EmpId);
                string carType = "";
                if (transferH.CarType == 1) carType = "รถกระบะ";
                else if (transferH.CarType == 2) carType = "รถบรรทุก";
                else if (transferH.CarType == 3) carType = "รถเทรลเลอร์";
                html = html.Replace("@@CarType@@", carType);
                html = html.Replace("@@CompName@@", transferH.Company);
                html = html.Replace("@@CarRegistration@@", transferH.CarNo);
                html = html.Replace("@@CarBrand@@", transferH.CarBrand);

                string SendToDepartment = "";
                if (transferH.SendToDepartment == 1) SendToDepartment = "Office";
                else if (transferH.SendToDepartment == 2) SendToDepartment = "Engineer";
                else if (transferH.SendToDepartment == 3) SendToDepartment = "Factory";
                else if (transferH.SendToDepartment == 4) SendToDepartment = "Warehouse";
                else if (transferH.SendToDepartment == 5) SendToDepartment = "Other";
                html = html.Replace("@@SendTo@@", SendToDepartment);

                string tagItem = "";
                int num = 0;
                foreach (var d in transferH.TransferDetail.ToList())
                {
                    num++;
                    tagItem += "<tr>";
                    tagItem += "<td align='center' style='background-color:white;'>" + d.Seq + "</td>";
                    tagItem += "<td style='background-color:white;'>" + d.Product.ProductCode + " " + d.Product.ProductName + "</td>";
                    tagItem += "<td align='center' style='background-color:white;'>" + d.RequestQty + "</td>";
                    tagItem += "</tr>";
                }
                for (int i = 0; i <= 18 - num; i++)
                {
                    tagItem += "<tr><td style='background-color:white;'>&nbsp;</td><td style='background-color:white;'>&nbsp;</td><td style='background-color:white;'>&nbsp;</td></tr>";
                }
                tagItem += "<tr>";
                tagItem += "<td style='background-color:white;'></td>";
                tagItem += "<td align='right' style='background-color:white;'>รวม</td>";
                tagItem += "<td align='center' style='background-color:white;'></td>";
                tagItem += "</tr>";
                html = html.Replace("@@item@@", tagItem);



                StringReader sr = new StringReader(html);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "TransferOut.pdf");
            }
        }
        public FileResult PDFTransferIn(int id)
        {
            TransferHeader transferH = uow.Modules.Transfer.Get(id);

            String html = string.Empty;
            html = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/html/" + "TransferInHTML.html"));
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                html = html.Replace("@@ImageBanner@@", HttpContext.Server.MapPath("~/images/logo-banner.png"));
                html = html.Replace("@@ImageCheckbox@@", HttpContext.Server.MapPath("~/html/img/checkbox_0.gif"));

                html = html.Replace("@@TransferNo@@", transferH.TransferNo);
                html = html.Replace("@@Goto@@", transferH.ReceiveTo);
                string txtTransferDate = transferH.TransferDate == null ? "__________" : transferH.TransferDate.Value.Day.ToString("00") + "/" + transferH.TransferDate.Value.Month.ToString("00") + "/" + transferH.TransferDate.Value.Year;
                string txtTransferTime = transferH.TransferTime;

                html = html.Replace("@@TransferDateTime@@", txtTransferDate + " " + txtTransferTime);
                html = html.Replace("@@For@@", transferH.Reason);
                html = html.Replace("@@PersonName@@", transferH.EmpId);
                string carType = "";
                if (transferH.CarType == 1) carType = "รถกระบะ";
                else if (transferH.CarType == 2) carType = "รถบรรทุก";
                else if (transferH.CarType == 3) carType = "รถเทรลเลอร์";
                html = html.Replace("@@CarType@@", carType);
                html = html.Replace("@@CompName@@", transferH.Company);
                html = html.Replace("@@CarRegistration@@", transferH.CarNo);
                html = html.Replace("@@CarBrand@@", transferH.CarBrand);

                string SendToDepartment = "";
                if (transferH.SendToDepartment == 1) SendToDepartment = "Office";
                else if (transferH.SendToDepartment == 2) SendToDepartment = "Engineer";
                else if (transferH.SendToDepartment == 3) SendToDepartment = "Factory";
                else if (transferH.SendToDepartment == 4) SendToDepartment = "Warehouse";
                else if (transferH.SendToDepartment == 5) SendToDepartment = "Other";
                html = html.Replace("@@SendTo@@", SendToDepartment);

                string tagItem = "";
                int num = 0;
                foreach (var d in transferH.TransferDetail.ToList())
                {
                    num++;
                    tagItem += "<tr>";
                    tagItem += "<td  align='center' style='background-color:white;'>" + d.Seq + "</td>";
                    tagItem += "<td style='background-color:white;'>" + d.Product.ProductCode + " " + d.Product.ProductName + "</td>";
                    tagItem += "<td align='center' style='background-color:white;'>" + d.RequestQty + "</td>";
                    tagItem += "</tr>";
                }
                for (int i = 0; i <= 18 - num; i++)
                {
                    tagItem += "<tr><td style='background-color:white;'>&nbsp;</td><td style='background-color:white;'>&nbsp;</td><td style='background-color:white;'>&nbsp;</td></tr>";
                }
                tagItem += "<tr>";
                tagItem += "<td style='background-color:white;'></td>";
                tagItem += "<td align='right' style='background-color:white;'>รวม</td>";
                tagItem += "<td align='center' style='background-color:white;'></td>";
                tagItem += "</tr>";
                html = html.Replace("@@item@@", tagItem);





                StringReader sr = new StringReader(html);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "TransferOut.pdf");
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