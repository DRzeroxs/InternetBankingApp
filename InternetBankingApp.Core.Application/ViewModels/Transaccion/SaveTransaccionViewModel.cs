﻿using InternetBankingApp.Core.Application.ViewModels.Cliente;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.Transaccion
{
    public class SaveTransaccionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Seleccione un metodo")]
        [Range(1, 3, ErrorMessage ="Seleccione un metodo valido")]
        public int Tipe {  get; set; }

        [Required(ErrorMessage = "Ingrese una cantidad")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una cantidad valida")]
        public double Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int CienteId { get; set; }
        public int CuentaOrigenId { get; set; }
        public int CuentaDestinoId { get; set; }

        //Visores
        public ClienteViewModel? Cliente { get; set; }
        public CuentaDeAhorroViewModel? CuentaOrige { get; set; }
        public CuentaDeAhorroViewModel? CuentaDestino { get; set; }
    }
}