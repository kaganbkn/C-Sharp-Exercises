using System;
using System.Collections.Generic;
using System.Text;

namespace dip.src
{ 
    //with open close principle
    public class InvoiceOCP
    {
        public virtual double GetInvoiceDiscount(double amount)
        {
            return amount;
        }
    }
    public class FinalInvoice : InvoiceOCP
    {
        public override double GetInvoiceDiscount(double amount)
        {
            return base.GetInvoiceDiscount(amount)-10;
        }
    }
    public class ProposedInvoice : InvoiceOCP
    {
        public override double GetInvoiceDiscount(double amount)
        {
            return base.GetInvoiceDiscount(amount)-20;
        }
    }
    public class RecurringInvoice : InvoiceOCP
    {
        public override double GetInvoiceDiscount(double amount)
        {
            return base.GetInvoiceDiscount(amount)-30;
        }
    }
}
