using System.Collections.Generic;


//*******************************************************************************
//프로그램명    MenuInfo.cs
//메뉴ID        
//설명          메뉴 권한 정보
//작성일        2012.12.06
//개발자        남인호
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//
//
//*******************************************************************************



namespace KR_POP
{
    // 요약:
    //     Presentation Layer(Win or Web)에서 사용되는 메뉴항목에 대한 정의 클래스 입니다.
    public class MenuInfo
    {
        /// <summary>
		/// 기본값으로 MenuInfo 클래스의 새 인스턴스를 초기화 합니다.
        /// </summary>
    
        public MenuInfo()
        {
            //
        }

        /// <summary>
		/// 시스템구분을 설정하거나 가지고 옵니다.
        /// </summary>
        public string SysGubun { get; set; }
        /// <summary>
		/// 메뉴ID를 설정하거나 가지고 옵니다.
        /// </summary>
        public string MenuID { get; set; }
        /// <summary>
		/// 메뉴이름을 설정하거나 가지고 옵니다.
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
		/// 계층구조 메뉴에서 현재 메뉴의 부모메뉴를 설정하거나 가지고 옵니다.
        /// </summary>
        public string ParentMenuID { get; set; }
        /// <summary>
		/// 메뉴가 자식메뉴를 소유하고 있는지 여부를 설정하거나 가지고 옵니다.
        /// </summary>
        public bool hasChild { get; set; }
        // 요약:
        //     메뉴를 통해 동적으로 호출할 어셈블리명을 설정하거나 가지고 옵니다.
        public string FormName { get; set; }
        /// <summary>
		/// 메뉴의 정렬순서를 설정하거나 가지고 옵니다.
        /// </summary>
        public int OrderSeq { get; set; }
		/// <summary>
		/// 메뉴의 업무구분을 설정하거나 가지고 옵니다.
		/// 한 화면을 여러 메뉴로 등록했을 때 사용합니다.
		/// </summary>
		public string JobGbn { get; set; }
        /// <summary>
		/// 현재 메뉴의 프로그램이 조회작업을 허용하는지 여부를 설정하거나 가지고 옵니다.
        /// </summary>
        public bool Read { get; set; }
        /// <summary>
		/// 현재 메뉴의 프로그램이 입력작업을 허용하는지 여부를 설정하거나 가지고 옵니다.
        /// </summary>
        public bool Insert { get; set; }
        /// <summary>
		/// 현재 메뉴의 프로그램이 수정작업을 허용하는지 여부를 설정하거나 가지고 옵니다.
        /// </summary>
        public bool Update { get; set; }
        /// <summary>
		/// 현재 메뉴의 프로그램이 삭제작업을 허용하는지 여부를 설정하거나 가지고 옵니다.
        /// </summary>
        public bool Delete { get; set; }
        /// <summary>
		/// 현재 메뉴의 프로그램이 저장작업을 허용하는지 여부를 설정하거나 가지고 옵니다.
        /// </summary>
        public bool Save { get; set; }
        /// <summary>
		/// 현재 메뉴의 프로그램이 출력작업을 허용하는지 여부를 설정하거나 가지고 옵니다.
        /// </summary>
        public bool Print { get; set; }
        /// <summary>
		/// 현재 메뉴의 프로그램이 Excel Export작업을 허용하는지 여부를 설정하거나 가지고 옵니다.
        /// </summary>
        public bool Excel { get; set; }
        /// <summary>
		/// 메뉴의 사용여부를 설정하거나 가지고 옵니다.
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
		/// 메뉴항목이 Presentation Layer환경에서 노출될지 여부를 설정하거나 가지고 옵니다.
        /// </summary>
        public bool IsShow { get; set; }

		/// <summary>
		/// 지정된 메뉴리스트 항목에서 지정된 메뉴ID와 일치하는 MenuInfo정보를 반환합니다.
		/// 일치하는 항목이 없을 경우 null를 반환합니다.
		/// </summary>
		/// <param name="menuList">대상 Menu List</param>
		/// <param name="menuId">찾을 Menu ID</param>
		/// <returns>일치하는 MenuInfo</returns>
        public static MenuInfo Find(IList<MenuInfo> menuList, string menuId)
        {
            MenuInfo ResultValue = null;

			foreach (MenuInfo menu in menuList)
			{
				if (menu.MenuID.Equals(menuId) == true)
				{
					ResultValue = menu;
					break;
				}
			}

            return ResultValue;
        }

		/// <summary>
		/// FormName을 이용하여 MenuInfo를 구한다.
		/// </summary>
		/// <param name="menuList"></param>
		/// <param name="formName"></param>
		/// <returns></returns>
		public static MenuInfo FindByFormName(IList<MenuInfo> menuList, string formName)
		{
			MenuInfo ResultValue = null;

			foreach (MenuInfo menu in menuList)
			{
				if (menu.FormName.Equals(formName) == true)
				{
					ResultValue = menu;
					break;
				}
			}

			return ResultValue;
		}

		/// <summary>
		/// 지정된 메뉴리스트 항목에서 지정된 프로그램ID와 일치하는 MenuInfo정보를 삭제합니다.
		/// </summary>
		/// <param name="menuList">대상 Menu List</param>
		/// <param name="menuId">찾을 Menu ID</param>
		/// <returns>처리결과</returns>
        public static bool Remove(IList<MenuInfo> menuList, string menuId)
        {
            MenuInfo ResultValue = null;

            for (int i = (menuList.Count-1); i >= 0; i--)
            {
                if (menuList[i].ParentMenuID == menuId)
                {
                    menuList.RemoveAt(i);
                }
            }

            for (int i = 0; i < menuList.Count; i++)
            {
                if (menuList[i].MenuID == menuId)
                {
                    ResultValue = menuList[i];

                    break;
                }
            }

            return true;
        }
    }
}
