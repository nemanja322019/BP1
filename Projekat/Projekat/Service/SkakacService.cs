using Projekat.DAO;
using Projekat.DAO.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Service
{
    public class SkakacService
    {
        private static readonly ISkakacDAO skakacDAO = new SkakacDAOImpl();

        public void NoviPBSC(string s,double PBSC)
        {
            skakacDAO.NoviPBSC(s,PBSC);
        }

    }
}
