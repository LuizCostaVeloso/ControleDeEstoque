using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MetodosApoio
    {
        /// <summary>
        /// Retorna a descrição de um determinado elemento de um Enumerador.
        /// </summary>
        /// <param name="elemento">Elemento do enumerador de onde a descrição será retornada.</param>
        /// <returns>String com a descrição do elemento do Enumerador.</returns>
        public static string DescricaoEnum(Enum elemento)
        {
            // Pego a chave do elemento passado
            FieldInfo infoElemento = elemento.GetType().GetField(elemento.ToString());

            // Conforme o elemento passado pego o atributo descrição
            DescriptionAttribute[] atributos = (DescriptionAttribute[])infoElemento.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // Se existe algum atributo
            if (atributos.Length > 0)
            {
                // Verifico se existe alguma descrição, pode haver enumeradores onde não haja descrição
                if (atributos[0].Description != null)
                {
                    return atributos[0].Description;
                }
                // Se não há descrição, retorno texto
                else
                {
                    return "Sem descrição";
                }
            }
            else
            {
                return elemento.ToString();
            }
        }

        /// <summary>
        /// Retorna uma lista com os valores de um determinado enumerador.
        /// </summary>
        /// <param name="enumerador">Enumerador que terá os valores listados.</param>
        /// <returns>Lista com descrição e valores dos elementos do enumerador.</returns>
        public static IList ListaElementos(Type enumerador)
        {
            // Crio a lista
            ArrayList lista = new ArrayList();

            // Verifico se o enumerador passado não é nulo
            if (enumerador != null)
            {
                // Jogos todos os elementos para uma lista (Somente os valores)
                Array enumValores = Enum.GetValues(enumerador);

                // Percorre todo o enumerador para pegar a Descrição
                foreach (Enum valor in enumValores)
                {
                    // Adiciono na lista o conteúdo retornado pelo método que pega a descrição
                    lista.Add(new KeyValuePair<Enum, string>(valor, DescricaoEnum(valor)));
                }
            }

            return lista;
        }
    }
}

