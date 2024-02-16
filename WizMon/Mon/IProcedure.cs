using System;

namespace WizMon
{
    public interface IProcedure
    {
        // 요약:
        //     화면 초기화작업
        void procClear();
        //
        // 요약:
        //     입력작업
        void procInsert();
        //
        // 요약:
        //     수정작업
        void procUpdate();
        //
        // 요약:
        //     삭제작업
        void procDelete();
        //
        // 요약:
        //     저장작업
        void procSave();
        //
        // 요약:
        //     조회작업
        void procQuery();
        //
        // 요약:
        //     출력작업
        void procPrint();
        //
        // 요약:
        //     파일변환작업
        void procExcel();
    }
}
