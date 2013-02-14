using System;
using System.Linq;
using System.Web.UI.WebControls;
using ClientDependency.Core;
using umbraco.BusinessLogic;
using umbraco.IO;
using umbraco.presentation.masterpages;
using umbraco.uicontrols;
using System.Web;

namespace Our.Umbraco.Sisyphus
{
	public class Application : ApplicationBase
	{
		public Application()
		{
			umbracoPage.Init += new MasterPageLoadHandler(this.RegisterSisyphus);
		}

		protected void RegisterSisyphus(object sender, EventArgs e)
		{
			// make sure the page is an umbracoPage; otherwise exit
			var page = sender as umbracoPage;
			if (page == null)
				return;

			// specify the pages that Sisyphus is allowed to be used on
			var allowedPages = new[] { "editcontent.aspx", "editmedia.aspx", "editmember.aspx" };
			var path = page.Page.Request.Path.ToLower();

			// check thath the path is allowed
			if (!allowedPages.Any(path.Contains))
				return;

			// make sure there is a body container; otherwise exit
			var container = page.FindControl("body") as ContentPlaceHolder;
			if (container == null)
				return;

			// attempt to find/create the ClientDependency loader for the page
			bool created;
			var loader = UmbracoClientDependencyLoader.TryCreate(page, out created);
			if (loader != null)
			{
				// set the path for Sisyphus ... and load the scripts
				loader.AddPath("Sisyphus", IOHelper.ResolveUrl(string.Concat(SystemDirectories.Umbraco, "/plugins/sisyphus")));
				loader.RegisterDependency(998, "sisyphus.min.js", "Sisyphus", ClientDependencyType.Javascript);
				loader.RegisterDependency(999, "loader.js", "Sisyphus", ClientDependencyType.Javascript);
			}
		}
	}
}