using System.Text;

namespace WillPower
{
    /// <summary>
    /// A container for uncommonly used <see cref="System.Text.Encoding">Encodings</see>.
    /// </summary>
    public sealed class Encodings
    {
        /// <summary>
        /// The EBCDIC <see cref="System.Text.Encoding">Encoding</see> for easy reference. 
        /// This is usually the 
        /// <see href="https://www.ibm.com/support/knowledgecenter/SSEQ5Y_5.9.0/com.ibm.pcomm.doc/reference/pdf/hcp_referenceV58.pdf">
        /// IBM 037</see> code page in .Net.
        /// Default is CodePage IBM037.
        /// </summary>
        public static Encoding EBCDIC
        {
            get
            {
#if NETSTANDARD
                if (!registered)
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    registered = true;
                }
#endif

                if (ebcdicEncoding == null)
                {
                    ebcdicEncoding = Encoding.GetEncoding("IBM037");
                }
                return ebcdicEncoding;
            }
            set
            {
                ebcdicEncoding = value;
            }
        }
        private static Encoding ebcdicEncoding = null;
        private static bool registered = false;

        /// <summary>
        /// ANSI (Default) <see cref="System.Text.Encoding">Encoding</see> for easy reference. 
        /// </summary>
        public static Encoding ANSI
        {
            get
            {
                return Encoding.Default;
            }
        }

    }
}
