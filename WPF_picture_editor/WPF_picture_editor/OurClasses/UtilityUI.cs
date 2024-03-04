using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_picture_editor.OurClasses {
    class UtilityUI {
        /// <summary>
        /// Metody volané UI komponentami (Logika pro ty metody!!!!)
        /// </summary>

        // Vlastnosti
        Filtering filtering;
        FileLoader fileLoader;

        // Konstruktor
        public UtilityUI() {
            filtering = new Filtering();
            fileLoader = new FileLoader();
        }
        // Metody


    }


}
