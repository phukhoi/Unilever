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
        public void AddDistributor(string name, string email, string phone, string addr)
        {
            try
            {
                Distributor dt = new Distributor
                    {
                        Name = name,
                        Email = email,
                        Phone = phone,
                        Addr = addr
                    };
                this.UnileverEntities.Distributors.Add(dt);
                this.UnileverEntities.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Product GetProductById(int ID)
        {
            Product p = UnileverEntities.Products.Where(pr => pr.ID == ID).FirstOrDefault();
            return p;
        }
        public bool SaveChanges(int? proId, string name, string catid, string price,
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
                            p = this.UnileverEntities.Products.Where(p1 => p1.ID == proId.Value).FirstOrDefault();
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
                            p = this.UnileverEntities.Products.Where(p1 => p1.ID == proId.Value).FirstOrDefault();
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
    }
}
