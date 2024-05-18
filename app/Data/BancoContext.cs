using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Data
{
    public class BancoContext : DbContext
    {

        //Fazendo injecao do Banco de Dados
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        //Adicionando a  depedencia dos Users
        public DbSet<UserModel> Users { get; set; }

    }
}