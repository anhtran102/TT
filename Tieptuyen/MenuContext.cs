using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Tieptuyen
{
    public class MenuContext
    {      
        public delegate void RefreshGridView();
        public RefreshGridView refresh;

        public delegate void xemthongtingiaodich();
        public xemthongtingiaodich xemTTGD;

        public delegate void tanggiamluong();
        public tanggiamluong TGL;

        public delegate void danhgianhanvien();
        public danhgianhanvien DGNV;

        public delegate void chuyencongtac();
        public chuyencongtac CCT;

        public delegate void nghicophep();
        public nghicophep NCP;

        public delegate void nghikhongphep();
        public nghikhongphep NKP;

        public delegate void nghicophepcathang();
        public nghicophepcathang NCPCTH;

        public delegate void nghikhongphepcathang();
        public nghikhongphepcathang NKPCTH;

        public delegate void nghicophepcatuan();
        public nghicophepcatuan NCPCTU;

        public delegate void nghikhongphepcatuan();
        public nghikhongphepcatuan NKPCTU;

        public delegate void nghitheothoigian();
        public nghitheothoigian NTTG;

        public delegate void khenthuongkyluat();
        public khenthuongkyluat KTKL;

        public delegate void xemphieunhaphang();
        public xemphieunhaphang xemPN;

        public delegate void chinhsuasanpham();
        public chinhsuasanpham chinhSP;

        public delegate void xemphieuxuathang();
        public xemphieuxuathang xemPX;

        public delegate void removegiohang();
        public removegiohang remove;

        public delegate void xuathoadon();
        public xuathoadon xuatHD;

        public delegate void Calltinhtien();
        public Calltinhtien calltinhtien;

        public delegate void saveSP();
        public saveSP save;

        public delegate void DeleteGridView();
        public DeleteGridView deleteGridView;

        public delegate void FillGridViewComboBox();
        public FillGridViewComboBox fillComboBox;

        public delegate void RemoveGroupEvaluateEvent(bool isRemove);
        public RemoveGroupEvaluateEvent removeGroupEvaluateEvent;
        bool istinhtien = false;

        public bool Istinhtien
        {
            get { return istinhtien; }
            set { istinhtien = value; }
        }


        bool isxemthongtingiaodich = false;
        public bool IsxemTTGD
        {
            get { return isxemthongtingiaodich; }
            set { isxemthongtingiaodich = value; }
        }
        bool isKTKL = false;

        public bool IsKTKL
        {
            get { return isKTKL; }
            set { isKTKL = value; }
        }

        bool isNCP = false;

        public bool IsNCP
        {
            get { return isNCP; }
            set { isNCP = value; }
        }

        bool isNCPCTH = false;

        public bool IsNCPCTH
        {
            get { return isNCPCTH; }
            set { isNCPCTH = value; }
        }

        bool isNKPCTH = false;

        public bool IsNKPCTH
        {
            get { return isNKPCTH; }
            set { isNKPCTH = value; }
        }

        bool isNCPCTU = false;

        public bool IsNCPCTU
        {
            get { return isNCPCTU; }
            set { isNCPCTU = value; }
        }

        bool isNKPCTU = false;

        public bool IsNKPCTU
        {
            get { return isNKPCTU; }
            set { isNKPCTU = value; }
        }

        bool isNTTG = false;

        public bool IsNTTG
        {
            get { return isNTTG; }
            set { isNTTG = value; }
        }

        bool isNKP = false;

        public bool IsNKP
        {
            get { return isNKP; }
            set { isNKP = value; }
        }



        bool isCCT = false;

        public bool IsCCT
        {
            get { return isCCT; }
            set { isCCT = value; }
        }

        bool isTGL = false;

        public bool IsTGL
        {
            get { return isTGL; }
            set { isTGL = value; }
        }

        bool isDGNV = false;

        public bool IsDGNV
        {
            get { return isDGNV; }
            set { isDGNV = value; }
        }

        bool isReport = false;

        public bool IsReport
        {
            get { return isReport; }
            set { isReport = value; }
        }

        bool ischinhsua = false;

        public bool Ischinhsua
        {
            get { return ischinhsua; }
            set { ischinhsua = value; }
        }

        bool isxemphieunhap = false;

        public bool Isxemphieunhap
        {
            get { return isxemphieunhap; }
            set { isxemphieunhap = value; }
        }
        bool isxemphieuxuat = false;

        public bool Isxemphieuxuat
        {
            get { return isxemphieuxuat; }
            set { isxemphieuxuat = value; }
        }

        bool isxuathoadon = false;
                public bool Isxuathoadon
        {
            get { return isxuathoadon; }
            set { isxuathoadon = value; }
        }

        bool isMutipleEdit = false;

        public bool IsMutipleEdit
        {
            get { return isMutipleEdit; }
            set { isMutipleEdit = value; }
        }
        private RadGridView grid;
        bool isCreateForm = true;
        public bool IsCreateForm
        {
            get { return isCreateForm; }
            set { isCreateForm = value; }
        }

        bool reset = false;



        bool tongcot = false;

        public bool istongcot
        {
            get { return tongcot; }
            set { tongcot = value; }
        }
        bool isdelete = false;

        public bool IsDelete
        {
            get { return isdelete; }
            set { isdelete = value; }
        }

               bool issuagiohang = false;

        public bool suagiohang
        {
            get { return issuagiohang; }
            set { issuagiohang = value; }
        }
        bool isRefresh = false;

        public bool IsRefresh
        {
            get { return isRefresh; }
            set { isRefresh = value; }
        }

        bool isColumnChooser = false;

        public bool IsColumnChooser
        {
            get { return isColumnChooser; }
            set { isColumnChooser = value; }
        }
        bool isSummaryRow = false;

        public bool IsSummaryRow
        {
            get { return isSummaryRow; }
            set { isSummaryRow = value; }
        }

        bool isResetLayout = false;

        public bool IsResetLayout
        {
            get { return isResetLayout; }
            set { isResetLayout = value; }
        }
        bool isSaveLayout = false;

        public bool IsSaveLayout
        {
            get { return isSaveLayout; }
            set { isSaveLayout = value; }
        }
        public MenuContext(RadGridView grid)
        {
            this.grid = grid;
        }

        bool isAdminReport = true;

         public bool IsAdminReport
        {
            get { return isAdminReport; }
            set { isAdminReport = value; }
        }

         public void GetReportMenu(RadDropDownMenu currentMenu)
         {
             RadMenuItem delete = null;
             RadMenuItem forms = null;      
             RadMenuItem summaryRows = null;
             RadMenuItem xemphieunhap = null;
             RadMenuItem xemphieuxuat = null;
             RadMenuItem tinhtien = null;
             RadMenuItem XHD = null;
             RadMenuItem xemthongtingiaodich = null;

             RadMenuItem NKP = null;
             RadMenuItem NCP = null;

             RadMenuItem chinhsua = null;
             RadMenuItem KTTL = null;

             RadMenuItem refreshItem = null;
             RadMenuItem TGL = null;
             RadMenuItem DG = null;
             RadMenuItem CCT = null;

             RadMenuItem NCPCTH = null;
             RadMenuItem NKPCTH = null;
             RadMenuItem NCPCTU = null;
             RadMenuItem NKPCTU = null;
             RadMenuItem NTTG = null;

             RadMenuItem bestFit = null;

             RadMenuItem columnChooser = null;

             RadMenuItem saveSP = null;
             RadMenuItem itemRp = null;
             RadMenuItem itemSubReport = null;
             RadMenuItem resetLayout = null;
          //   RadMenuItem delete = null;
             if (currentMenu == null) return;
             if (this.grid.CurrentCell == null) return;
             if (this.grid.CurrentCell != null)
             {

                 if (this.grid.CurrentCell.RowElement is GridFilterRowElement)
                 {
                     var itemFilter = from x in currentMenu.Items
                                      where x.Text == "Group by this column" || x.Text == "Pin Column"
                                      select x;
                     foreach (RadItem x in itemFilter.ToList<RadItem>())
                         currentMenu.Items.Remove(x);
                     itemFilter = null;
                     if (isRefresh)
                     {
                         refreshItem = new RadMenuItem("Làm mới");

                         refreshItem.Click += new EventHandler(refreshItem_Click);
                         currentMenu.Items.Add(refreshItem);
                     }
                     return;
                 }


                
                 currentMenu.Items.Clear();
                 if (isRefresh)
                 {
                     refreshItem = new RadMenuItem("Làm mới");
                     refreshItem.Click += new EventHandler(refreshItem_Click);
                     currentMenu.Items.Add(refreshItem);
                 }
                     //return;
                     bestFit = new RadMenuItem("Tự giãn tất cả các cột");
                 bestFit.Click += new EventHandler(bestFit_Click);
                 currentMenu.Items.Add(bestFit);
                 if (isSummaryRow)
                 {
                     summaryRows = new RadMenuItem("Tổng số");
                     summaryRows.Click += new EventHandler(summaryRows_Click);
                     currentMenu.Items.Add(summaryRows);
                 }
                     if (suagiohang == true)
                     {
                         saveSP = new RadMenuItem("Cập Nhật Thông Tin");
                         saveSP.Click += new EventHandler(saveSP_Click);
                         currentMenu.Items.Add(saveSP);
                     }
                     if (isxemthongtingiaodich == true)
                     {
                         xemthongtingiaodich = new RadMenuItem("Xem Thông Tin Giao Dịch");
                         xemthongtingiaodich.Click += new EventHandler(xemthongtingiaodich_Click);
                         currentMenu.Items.Add(xemthongtingiaodich);
                     }

                     if (isTGL == true)
                     {
                         TGL = new RadMenuItem("Tăng - Giảm Lương");
                         TGL.Click += new EventHandler(TGL_Click);
                         currentMenu.Items.Add(TGL);
                     }

                     if (isNCP == true)
                     {
                         NCP = new RadMenuItem("Nghĩ Có Phép Cả Ngày");
                         NCP.Click += new EventHandler(NCP_Click);
                         currentMenu.Items.Add(NCP);
                     }

                     if (isNKP == true)
                     {
                         NKP = new RadMenuItem("Nghĩ Không Phép Cả Ngày");
                         NKP.Click += new EventHandler(NKP_Click);
                         currentMenu.Items.Add(NKP);
                     }

                     if (isNTTG == true)
                     {
                         NTTG = new RadMenuItem("Nghĩ Theo Thời Gian");
                         NTTG.Click += new EventHandler(NTTG_Click);
                         currentMenu.Items.Add(NTTG);
                     }

                     if (isNCPCTH == true)
                     {
                         NCPCTH = new RadMenuItem("Nghĩ Có Phép Cả Tháng");
                         NCPCTH.Click += new EventHandler(NCPCTH_Click);
                         currentMenu.Items.Add(NCPCTH);
                     }

                     if (isNKPCTH == true)
                     {
                         NKPCTH = new RadMenuItem("Nghĩ Không Phép Cả Tháng");
                         NKPCTH.Click += new EventHandler(NKPCTH_Click);
                         currentMenu.Items.Add(NKPCTH);
                     }

                     if (isNCPCTU == true)
                     {
                         NCPCTU = new RadMenuItem("Nghĩ Có Phép Cả Tuần");
                         NCPCTU.Click += new EventHandler(NCPCTU_Click);
                         currentMenu.Items.Add(NCPCTU);
                     }

                     if (isNKPCTU == true)
                     {
                         NKPCTU = new RadMenuItem("Nghĩ Không Phép Cả Tuần");
                         NKPCTU.Click += new EventHandler(NKPCTU_Click);
                         currentMenu.Items.Add(NKPCTU);
                     }

                     if (isDGNV == true)
                     {
                         DG = new RadMenuItem("Đánh Giá Nhân Viên");
                         DG.Click += new EventHandler(DG_Click);
                         currentMenu.Items.Add(DG);
                     }

                     if (isCCT == true)
                     {
                         CCT = new RadMenuItem("Chuyển Công Tác");
                         CCT.Click += new EventHandler(CCT_Click);
                         currentMenu.Items.Add(CCT);
                     }

                     if (isdelete)
                     {
                         delete = new RadMenuItem("Xóa");
                         delete.Click += new EventHandler(delete_Click);
                         currentMenu.Items.Add(delete);
                     }
                     if (istinhtien)
                     {
                         tinhtien = new RadMenuItem("tính tiền");
                         tinhtien.Click += new EventHandler(tinhtien_Click);
                         currentMenu.Items.Add(tinhtien);
                     }
                     if (isxuathoadon)
                     {
                         XHD = new RadMenuItem("xuất hóa đơn");
                         XHD.Click += new EventHandler(XHD_Click);
                         currentMenu.Items.Add(XHD);
                     }
                     if (isxemphieunhap)
                     {
                         xemphieunhap = new RadMenuItem("Xem chi tiết phiếu nhập hàng");
                         xemphieunhap.Click += new EventHandler(xemphieunhap_Click);
                         currentMenu.Items.Add(xemphieunhap);
                     }
                     if (isxemphieuxuat)
                     {
                         xemphieuxuat = new RadMenuItem("Xem chi tiết phiếu xuất hàng");
                         xemphieuxuat.Click += new EventHandler(xemphieuxuat_Click);
                         currentMenu.Items.Add(xemphieuxuat);
                     }

                     if (isReport)
                     {
                         itemRp = new RadMenuItem("Xuất báo cáo");
                         itemRp.Tag = 1;
                         itemRp.Click += new EventHandler(itemRp_Click);
                         currentMenu.Items.Add(itemRp);
                         itemSubReport = new RadMenuItem("Xuất báo cáo theo dòng đã chọn");
                         itemSubReport.Tag = 2;
                         itemSubReport.Click += new EventHandler(itemSubReport_Click);
                     }

                     if (ischinhsua)
                     {
                         chinhsua = new RadMenuItem("Chỉnh Sửa ");
                         chinhsua.Click += new EventHandler(chinhsua_Click);
                         currentMenu.Items.Add(chinhsua);
                     }
                     if (isKTKL)
                     {
                         KTTL = new RadMenuItem("Trả Hàng");
                         KTTL.Click += new EventHandler(KTTL_Click);
                         currentMenu.Items.Add(KTTL);
                     }
             }
    

         }
         void xemphieunhap_Click(object sender, EventArgs e)
         {
             xemPN();
         }

         void xemthongtingiaodich_Click(object sender, EventArgs e)
         {
             xemTTGD();
         }
         void chinhsua_Click(object sender, EventArgs e)
         {
             chinhSP();
         }
         void KTTL_Click(object sender, EventArgs e)
         {
             KTKL();
         }
         void xemphieuxuat_Click(object sender, EventArgs e)
         {
             xemPX();
         }

         void TGL_Click(object sender, EventArgs e)
         {
             
             TGL();
         }

         void NCP_Click(object sender, EventArgs e)
         {

             NCP();
         }

         void NKP_Click(object sender, EventArgs e)
         {

             NKP();
         }

         void NTTG_Click(object sender, EventArgs e)
         {

             NTTG();
         }

         void NCPCTH_Click(object sender, EventArgs e)
         {

             NCPCTH();
         }
         void NKPCTH_Click(object sender, EventArgs e)
         {

             NKPCTH();
         }
         void NCPCTU_Click(object sender, EventArgs e)
         {

             NCPCTU();
         }
         void NKPCTU_Click(object sender, EventArgs e)
         {

             NKPCTU();
         }

         void DG_Click(object sender, EventArgs e)
         {
             
             DGNV();
         }

         void CCT_Click(object sender, EventArgs e)
         {
             
             CCT();
         }
        

         void XHD_Click(object sender, EventArgs e)
         {
             xuatHD();
         }
         void delete_Click(object sender, EventArgs e)
         {
             remove();
         }
         void tinhtien_Click(object sender, EventArgs e)
         {
             calltinhtien();
         }
         void saveSP_Click(object sender, EventArgs e)
         {
             save();
         }
         void refreshItem_Click(object sender, EventArgs e)
         {
             if (refresh != null) refresh();

         }
         void bestFit_Click(object sender, EventArgs e)
         {
             if (grid != null) grid.MasterGridViewTemplate.BestFitColumns();
         }
         void summaryRows_Click(object sender, EventArgs e)
         {
             if (removeGroupEvaluateEvent == null) return;
             SummaryRowForm form = new SummaryRowForm(grid);
             form.removeGroupEvaluateEvent = new SummaryRowForm.RemoveGroupEvaluateEvent(removeGroupEvaluateEvent);
             form.ShowDialog();
         }
         void itemRp_Click(object sender, EventArgs e)
         {
             MachineApplication.ReportViewer(grid);
         }
         void itemSubReport_Click(object sender, EventArgs e)
         {

             //if (selectedPerson == null) return;

             //SubReportPersonForm subReportForm = new SubReportPersonForm();
             //subReportForm.SelectedPerson = selectedPerson;
             //subReportForm.LoadGridView(grid);
             //subReportForm.ShowDialog();
         }
    }
}
