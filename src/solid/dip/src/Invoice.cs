using System;
using System.Collections.Generic;
using System.Text;

namespace dip.src
{
    public class Invoice
    {
        public double GetInvoiceDiscount(double amount,InvoiceType invoiceType)
        {
            if (invoiceType==InvoiceType.FinalInvoice)
            {
                amount -= amount * 0.1;
            }
            else if (invoiceType == InvoiceType.ProposedInvoice)
            {

                amount -= amount * 0.2;
            }

            return amount;
        }

    }

    public enum InvoiceType {
        FinalInvoice,
        ProposedInvoice
    }
}
