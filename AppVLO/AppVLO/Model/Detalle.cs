using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppVLO.Model
{
    public class Detalle : INotifyPropertyChanged
    {

        
        private int _IdDetalle;
        [PrimaryKey,AutoIncrement]
        public int IdDetalle
        {
            get
            {
                return _IdDetalle;
            }
            set
            {
                _IdDetalle = value;
                OnPropertyChanged();
            }
        }
        private int _IdMenu;
        public int IdMenu
        {
            get
            {
                return _IdMenu;
            }
            set
            {
                _IdMenu = value;
                OnPropertyChanged();
            }
        }
        private int _IdPedido;
        public int IdPedido
        {
            get
            {
                return _IdPedido;
            }
            set
            {
                _IdPedido = value;
                OnPropertyChanged();
            }
        }
        private int _IdMesa;
        public int IdMesa
        {
            get
            {
                return _IdMesa;
            }
            set
            {
                _IdMesa = value;
                OnPropertyChanged();
            }
        }
        private int _cantidad;
        public int cantidad
        {
            get
            {
                return _cantidad;
            }
            set
            {
                _cantidad = value;
                OnPropertyChanged();
            }
        }
        private int _Estado;
        public int Estado
        {
            get
            {
                return _Estado;
            }
            set
            {
                _Estado = value;
                OnPropertyChanged();
            }
        }
        private string _Termino;
        public string Termino
        {
            get
            {
                return _Termino;
            }
            set
            {
                _Termino = value;
                OnPropertyChanged();
            }
        }
        private string _Comentarios;
        public string Comentarios
        {
            get
            {
                return _Comentarios;
            }
            set
            {
                _Comentarios = value;
                OnPropertyChanged();
            }
        }

        private string _Menu;
        public string Menu
        {
            get
            {
                return _Menu;
            }
            set
            {
                _Menu = value;
                OnPropertyChanged();
            }
        }
        private string _PrecioUnitario;
        public string PrecioUnitario
        {
            get
            {
                return _PrecioUnitario;
            }
            set
            {
                _PrecioUnitario = value;
                OnPropertyChanged();
            }
        }

        public string Precio {
            get { return $"Precio: ${PrecioUnitario}"; }
        }

        public string Canti
        {
            get { return $"Cantidad: {cantidad}"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
