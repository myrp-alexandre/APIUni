﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NFe.Components.Abstract;
using NFe.Components.br.com.simplissweb.wsbalneariocamboriu.p;
using System.Net;

namespace NFe.Components.SimplISS.BalnearioCamboriu.p
{
    public class SimplISSP : EmiteNFSeBase
    {
        #region Propriedades
        /// <summary>
        /// Objeto de conexão com o Webservice
        /// </summary>
        private NfseService Service = new NfseService();

        /// <summary>
        /// Dados de login e senha para autenticação
        /// </summary>
        private ddDuasStrings DadosConexao = new ddDuasStrings();

        /// <summary>
        /// Namespace utilizada para deserialização do objeto
        /// </summary>
        public override string NameSpaces
        {
            get
            {
                return "http://www.sistema.com.br/Nfse/arquivos/nfse_3.xsd";
            }
        }
        #endregion

        #region Construtores
        public SimplISSP(TipoAmbiente tpAmb, string pastaRetorno, string usuario, string senhaWs, string proxyuser, string proxypass, string proxyserver)
            : base(tpAmb, pastaRetorno)
        {
            if (!String.IsNullOrEmpty(proxyuser))
            {
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(proxyuser, proxypass, proxyserver);
                System.Net.WebRequest.DefaultWebProxy.Credentials = credentials;

                Service.Proxy = WebRequest.DefaultWebProxy;
                Service.Proxy.Credentials = new NetworkCredential(proxyuser, proxypass);
                Service.Credentials = new NetworkCredential(proxyuser, proxypass);
            }

            DadosConexao.P1 = usuario;
            DadosConexao.P2 = senhaWs;
        }
        #endregion

        #region Métodos
        public override void EmiteNF(string file)
        {
            GerarNovaNfseEnvio envio = DeserializarObjeto<GerarNovaNfseEnvio>(file);              
            string strResult = SerializarObjeto(Service.GerarNfse(envio, DadosConexao));

            GerarRetorno(file, strResult, Propriedade.Extensao(Propriedade.TipoEnvio.EnvLoteRps).EnvioXML,
                                          Propriedade.Extensao(Propriedade.TipoEnvio.EnvLoteRps).RetornoXML);

        }

        public override void CancelarNfse(string file)
        {
            CancelarNfseEnvio envio = DeserializarObjeto<CancelarNfseEnvio>(file);
            string strResult = SerializarObjeto(Service.CancelarNfse(envio, DadosConexao));

            GerarRetorno(file, strResult, Propriedade.Extensao(Propriedade.TipoEnvio.PedCanNFSe).EnvioXML,
                                          Propriedade.Extensao(Propriedade.TipoEnvio.PedCanNFSe).RetornoXML);
        }

        public override void ConsultarLoteRps(string file)
        {
            ConsultarLoteRpsEnvio envio = DeserializarObjeto<ConsultarLoteRpsEnvio>(file);
            string strResult = SerializarObjeto(Service.ConsultarLoteRps(envio, DadosConexao));
            GerarRetorno(file, strResult, Propriedade.Extensao(Propriedade.TipoEnvio.PedLoteRps).EnvioXML,
                                          Propriedade.Extensao(Propriedade.TipoEnvio.PedLoteRps).RetornoXML);
        }

        public override void ConsultarSituacaoLoteRps(string file)
        {
            ConsultarSituacaoLoteRpsEnvio envio = DeserializarObjeto<ConsultarSituacaoLoteRpsEnvio>(file);
            string strResult = SerializarObjeto(Service.ConsultarSituacaoLoteRps(envio, DadosConexao));
            GerarRetorno(file, strResult, Propriedade.Extensao(Propriedade.TipoEnvio.PedSitLoteRps).EnvioXML,
                                          Propriedade.Extensao(Propriedade.TipoEnvio.PedSitLoteRps).RetornoXML);
        }

        public override void ConsultarNfse(string file)
        {
            ConsultarNfseEnvio envio = DeserializarObjeto<ConsultarNfseEnvio>(file);
            string strResult = SerializarObjeto(Service.ConsultarNfse(envio, DadosConexao));
            GerarRetorno(file, strResult, Propriedade.Extensao(Propriedade.TipoEnvio.PedSitNFSe).EnvioXML,
                                          Propriedade.Extensao(Propriedade.TipoEnvio.PedSitNFSe).RetornoXML);
        }

        public override void ConsultarNfsePorRps(string file)
        {
            ConsultarNfseRpsEnvio envio = DeserializarObjeto<ConsultarNfseRpsEnvio>(file);
            string strResult = SerializarObjeto(Service.ConsultarNfsePorRps(envio, DadosConexao));
            GerarRetorno(file, strResult, Propriedade.Extensao(Propriedade.TipoEnvio.PedSitNFSeRps).EnvioXML,
                                          Propriedade.Extensao(Propriedade.TipoEnvio.PedSitNFSeRps).RetornoXML);
        }


        #region API

        public override XmlDocument EmiteNF(XmlDocument xml)
        {
            GerarNovaNfseEnvio envio = DeserializarObjeto<GerarNovaNfseEnvio>(xml);
            string strResult = SerializarObjeto(Service.GerarNfse(envio, DadosConexao));

            XmlDocument doc = new XmlDocument();
            doc.Load(strResult);

            return doc;

        }

        public override XmlDocument CancelarNfse(XmlDocument xml)
        {
            CancelarNfseEnvio envio = DeserializarObjeto<CancelarNfseEnvio>(xml);
            string strResult = SerializarObjeto(Service.CancelarNfse(envio, DadosConexao));

            XmlDocument doc = new XmlDocument();
            doc.Load(strResult);

            return doc;
        }

        public override XmlDocument ConsultarLoteRps(XmlDocument xml)
        {
            ConsultarLoteRpsEnvio envio = DeserializarObjeto<ConsultarLoteRpsEnvio>(xml);
            string strResult = SerializarObjeto(Service.ConsultarLoteRps(envio, DadosConexao));

            XmlDocument doc = new XmlDocument();
            doc.Load(strResult);

            return doc;
        }

        public override XmlDocument ConsultarSituacaoLoteRps(XmlDocument xml)
        {
            ConsultarSituacaoLoteRpsEnvio envio = DeserializarObjeto<ConsultarSituacaoLoteRpsEnvio>(xml);
            string strResult = SerializarObjeto(Service.ConsultarSituacaoLoteRps(envio, DadosConexao));

            XmlDocument doc = new XmlDocument();
            doc.Load(strResult);

            return doc;
        }

        public override XmlDocument ConsultarNfse(XmlDocument xml)
        {
            ConsultarNfseEnvio envio = DeserializarObjeto<ConsultarNfseEnvio>(xml);
            string strResult = SerializarObjeto(Service.ConsultarNfse(envio, DadosConexao));

            XmlDocument doc = new XmlDocument();
            doc.Load(strResult);

            return doc;
        }

        public override XmlDocument ConsultarNfsePorRps(XmlDocument xml)
        {
            ConsultarNfseRpsEnvio envio = DeserializarObjeto<ConsultarNfseRpsEnvio>(xml);
            string strResult = SerializarObjeto(Service.ConsultarNfsePorRps(envio, DadosConexao));

            XmlDocument doc = new XmlDocument();
            doc.Load(strResult);

            return doc;
        }

        #endregion

        #endregion
    }
}
