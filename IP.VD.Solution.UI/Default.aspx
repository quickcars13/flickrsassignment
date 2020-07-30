<%@ Page Title="Flickr Image Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IP.VD.Solution.UI._Default" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="FC" runat="server">
    <!-- Custom css -->
    <link rel="stylesheet" href="Content/assets/css/style.css" />

    <style type="text/css">
                        .hidden {
                    display: none;
                }

                .Hidden{
                    visibility:hidden;
                }
    </style>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/lightbox2/2.11.1/css/lightbox.min.css">

    <script src="Content/assets/js/jquery.min.js "></script>
    <script src="Content/assets/js/jquery.unveil.js"></script>
    <script src="Content/assets/js/aos.js"></script>
    <script src="Content/assets/js/swiper.min.js"></script>
    <script src="Content/assets/js/jquery.magnific-popup.min.js"></script>
    <script src="Content/assets/js/owl.carousel.min.js"></script>
    <script src="Content/assets/js/isotope.min.js"></script>
    <script src="Content/assets/js/imagesloaded.min.js"></script>
    <script src="Content/assets/js/carousel.js"></script>
    <script src="Content/assets/js/menu.js"></script>
    <script src="Content/assets/js/elements.js"></script>
    <script src="Content/assets/js/masonry.js"></script>
        <script src="Content/assets/js/form.js"></script>
    <script src="Scripts/axios.min.js"></script>
    <!--  Page Content  -->
    <script type="text/javascript">
$(document).ready(function() {
            
  var timeoutID = null;
    

  $('#searchButton').keyup(function(e) {
    clearTimeout(timeoutID);
    timeoutID = setTimeout(findImages.bind(undefined, e.target.value), 500);
  });

});
    </script>

    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="page-header--hfixedlg" data-padding="xltop" data-bg="grey">
                <div class="container">
                    <div class="row" data-padding="smbottom">
                        <div class="col-12 col-lg-6">
                            <div class="sectiontitle-element">
                                <h1>Flickr Image Search</h1>
                                <p>Utility Tool based on Lightbox JS, Bootstrap, SQL Server & C Sharp</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           
            <div class="page-content" data-padding="top">
                <div class="container">
                    <div class="row">
                        <div class="col-12 col-md-8 col-lg-9">

                            <div class="search-form--icon--v2--colorone--round searchFormMain">
                                <asp:TextBox  runat="server" ID="searchTextBox" TextMode="SingleLine" CssClass="searchForm" placeholder="Search for..." OnTextChanged="btnSearch_Click" AutoPostBack="true"></asp:TextBox>
                                <span class="submit-wrap">

                                    <asp:LinkButton ID="SubmitBtn" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-primary form-control">
                                        <i class="feather icon-search"></i>
                                    </asp:LinkButton>
                                  
                                  
                                </span>
                        
                            </div>

                            <asp:GridView ID="GV_ImageResults" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_ImageResults_RowDataBound" ShowFooter="true" AllowPaging="true" PageSize="10" OnPageIndexChanging="GV_ImageResults_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="ImageField" HeaderText="" HtmlEncode="False" />
                                </Columns>
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>

                            <div class="col-12" data-padding="sm">
                                <div class="separator-element"></div>
                            </div>
                        </div>
                        <!-- Sidebar -->
                        <div class="col-12 col-md-4 col-lg-3" data-hidden="md">
                            <aside data-padding="coltop">
                                <div class="widget-wrapper" data-padding="xsbottom">
                                    <h5>Note</h5>

                                    <blockquote class="blockquote-element--secondary text-justify"> Your query will be matched against the description, title, location, region and tags available from the Flickr Image
                                        <span class="author">Flickr App</span>
                                    </blockquote>
                                </div>
                            </aside>
                        </div>
                        <!-- END Sidebar -->
                    </div>
                </div>

            </div>
            <!--  END Page Content  -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
