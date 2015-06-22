using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnileverDAL;
using System.Data;
using UnileverObject;
using System.Linq.Expressions;

namespace UnileverBLL
{
    public class UnileverBLL : BLL, IBLL
    {
        public UnileverEntities UnileverEntities { get; set; }
        public UnileverBLL()
        {
            UnileverEntities = new UnileverEntities();
        }
        public List<Product> GetListProducts()
        {
            return UnileverEntities.Products.ToList();
        }
        public Category GetCategoryById(int catid)
        {
            return UnileverEntities.Categories.Where(c => c.ID == catid)
                .FirstOrDefault();
        }
        public List<Category> GetListCategories()
        {
            return UnileverEntities.Categories.ToList();
        }
        public List<Distributor> GetListDistributors()
        {
            return this.UnileverEntities.Distributors.ToList();
        }
        public bool SaveChangesDistributor(int distribId, string name, string email, string phone, string addr, CRUDOPTION option)
        {
            Distributor dt = null;
            try
            {
                switch (option)
                {
                    case CRUDOPTION.CREATE:
                        {
                            dt = new Distributor
                            {
                                Name = name,
                                Email = email,
                                Addr = addr,
                                Phone = phone
                            };
                            this.UnileverEntities.Distributors.Add(dt);
                            break;
                        }
                    case CRUDOPTION.UPDATE:
                        {
                            dt = GetDistributorById(distribId);
                            dt.Name = name;
                            dt.Email = email;
                            dt.Addr = addr;
                            dt.Phone = phone;
                            break;
                        }
                    case CRUDOPTION.DELETE:
                        {
                            dt = GetDistributorById(distribId);
                            this.UnileverEntities.Distributors.Remove(dt);
                            break;
                        }
                    default: break;
                }
                this.UnileverEntities.SaveChanges();
            }
            catch (Exception)
            {               
                throw;
            }
            return true;
        }

        public Distributor GetDistributorById(int distribId)
        {
            Distributor dt = this.UnileverEntities.Distributors.Where(d => d.ID == distribId).FirstOrDefault();
            return dt;
        }
        public Product GetProductById(int ID)
        {
            Product p = UnileverEntities.Products.Where(pr => pr.ID == ID).FirstOrDefault();
            return p;
        }
        public bool SaveChangesProduct(int proId, string name, string catid, string price,
          string importdate, string remain, string descript, CRUDOPTION option = CRUDOPTION.CREATE)
        {

            Product p = null;
            try
            {
                switch (option)
                {
                    case CRUDOPTION.CREATE:
                        {
                            p = new Product();
                            p.Name = name;
                            p.Descript = descript;
                            int rm;
                            int pr;
                            int cid;
                            int.TryParse(remain, out rm);
                            int.TryParse(price, out pr);
                            int.TryParse(catid, out cid);
                            p.Price = pr;
                            p.RemainingAmount = rm;
                            p.CatID = cid;
                            DateTime ipd = DateTime.Parse(importdate);
                            p.ImportDate = ipd;
                            this.UnileverEntities.Products.Add(p);
                            break;
                        }
                    case CRUDOPTION.UPDATE:
                        {
                            p = this.UnileverEntities.Products.Where(p1 => p1.ID == proId).FirstOrDefault();
                            p.Name = name;
                            p.Descript = descript;
                            int rm;
                            int pr;
                            int cid;
                            int.TryParse(remain, out rm);
                            int.TryParse(price, out pr);
                            int.TryParse(catid, out cid);
                            p.Price = pr;
                            p.RemainingAmount = rm;
                            p.CatID = cid;
                            break;
                        }
                    case CRUDOPTION.DELETE:
                        {
                            p = this.UnileverEntities.Products.Where(p1 => p1.ID == proId).FirstOrDefault();
                            this.UnileverEntities.Products.Remove(p);
                            break;
                        }
                    default: break;
                }
                this.UnileverEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public DefferredLiability GetDefferredLiabilityByOrderId(int ordID)
        {
            try
            {
                DefferredLiability dl = this.UnileverEntities.DefferredLiabilities
                        .Where(d => d.OrderID == ordID).FirstOrDefault();
                return dl;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public DefferredLiability GetDefferredLiabilityById(int defId)
        {
            try
            {
                DefferredLiability dl = this.UnileverEntities.DefferredLiabilities
                    .FirstOrDefault(d => d.ID == defId);
                return dl;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void UpdateDefferredLiability(int defid, string defdate, string resday)
        {
            try
            {
                DefferredLiability dl = GetDefferredLiabilityById(defid);
                dl.DebtDate = DateTime.Parse(defdate);
                int rd;
                int.TryParse(resday, out rd);
                if (dl.PeriodOfDebt.HasValue)
                {
                    dl.PeriodOfDebt += rd;
                }
                this.UnileverEntities.SaveChanges();
            }
            catch (NullReferenceException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<int> GetListDistributorId()
        {
            try
            {
                IEnumerable<int> res = from d in this.UnileverEntities.Distributors
                                       select d.ID;
                return res.ToList();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
