﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PaymentApplyProject.Application.Localizations {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PaymentApplyProject.Application.Localizations.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bu kayıt onaylanmış..
        /// </summary>
        internal static string AlreadyApproved {
            get {
                return ResourceManager.GetString("AlreadyApproved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bu kayıt reddedilmiş..
        /// </summary>
        internal static string AlreadyRejected {
            get {
                return ResourceManager.GetString("AlreadyRejected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Giriş bilgileriniz yanlıştır..
        /// </summary>
        internal static string IncorrectLoginInfo {
            get {
                return ResourceManager.GetString("IncorrectLoginInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sisteme giriş için yetkiniz yok..
        /// </summary>
        internal static string NotAuthorized {
            get {
                return ResourceManager.GetString("NotAuthorized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Veri bulunamadı..
        /// </summary>
        internal static string NotFound {
            get {
                return ResourceManager.GetString("NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} bulunamadı..
        /// </summary>
        internal static string NotFoundWithName {
            get {
                return ResourceManager.GetString("NotFoundWithName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to İşlem başarılı..
        /// </summary>
        internal static string OperationSuccessful {
            get {
                return ResourceManager.GetString("OperationSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bu müşteriye işlemler kapalıdır..
        /// </summary>
        internal static string PassiveCustomer {
            get {
                return ResourceManager.GetString("PassiveCustomer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mevcutta bekleyen işlem vardır..
        /// </summary>
        internal static string ThereIsPendingTransaction {
            get {
                return ResourceManager.GetString("ThereIsPendingTransaction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bu firma kodu tanımlı değil..
        /// </summary>
        internal static string ThisCodeNotDefined {
            get {
                return ResourceManager.GetString("ThisCodeNotDefined", resourceCulture);
            }
        }
    }
}
