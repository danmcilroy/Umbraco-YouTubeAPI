//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v8.9.1
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder.Embedded;

namespace Umbraco.Web.PublishedModels
{
	// Mixin Content Type with alias "videoContentControls"
	/// <summary>Video Content Controls</summary>
	public partial interface IVideoContentControls : IPublishedContent
	{
		/// <summary>Video Link URL</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		string VideoLinkUrl { get; }

		/// <summary>Video Thumbnail Image URL</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		string VideoThumbnailImageUrl { get; }
	}

	/// <summary>Video Content Controls</summary>
	[PublishedModel("videoContentControls")]
	public partial class VideoContentControls : PublishedContentModel, IVideoContentControls
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		public new const string ModelTypeAlias = "videoContentControls";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		public new static IPublishedContentType GetModelContentType()
			=> PublishedModelUtility.GetModelContentType(ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<VideoContentControls, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(), selector);
#pragma warning restore 0109

		// ctor
		public VideoContentControls(IPublishedContent content)
			: base(content)
		{ }

		// properties

		///<summary>
		/// Video Link URL: Enter a YouTube Link URL
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		[ImplementPropertyType("videoLinkURL")]
		public string VideoLinkUrl => GetVideoLinkUrl(this);

		/// <summary>Static getter for Video Link URL</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		public static string GetVideoLinkUrl(IVideoContentControls that) => that.Value<string>("videoLinkURL");

		///<summary>
		/// Video Thumbnail Image URL: Enter a YouTube video thumbnail image URL
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		[ImplementPropertyType("videoThumbnailImageURL")]
		public string VideoThumbnailImageUrl => GetVideoThumbnailImageUrl(this);

		/// <summary>Static getter for Video Thumbnail Image URL</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "8.9.1")]
		public static string GetVideoThumbnailImageUrl(IVideoContentControls that) => that.Value<string>("videoThumbnailImageURL");
	}
}
