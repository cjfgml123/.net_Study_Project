using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public enum SexTypes
    {

        None = 0,
        Male,
        FeMale

    }

    public class CommonData
    {

        DateTime createDate = DateTime.Now;
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        DateTime modifyDate = DateTime.Now;
        public DateTime ModifyDate
        {
            get { return modifyDate; }
            set { modifyDate = value; }
        }

        string id = string.Empty;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        string pw = string.Empty;
        public string PW
        {
            get { return pw; }
            set { pw = value; }
        }

        byte age = 0;
        public byte Age
        {
            get { return age; }
            set { age = value; }
        }

        bool ageChk = false;
        public bool AgeChk
        {
            get { return ageChk; }
            set { ageChk = value; }
        }

        float tall = -0.1f;
        public float Tall
        {
            get { return tall; }
            set { tall = value; }
        }
        SexTypes sexType = SexTypes.None;
        public SexTypes SexType
        {
            get { return sexType; }
            set { sexType = value; }
        }

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public CommonData()
        {

        }
        /// <summary>
        /// 오버라이딩 생성자
        /// </summary>
        /// <param name="_CreateDate">회원정보 생성날짜</param>
        /// <param name="_ModifyDate">회원정보 수정날짜</param>
        /// <param name="_id">생성 아이디</param>
        /// <param name="_pw">생성 비밀번호</param>
        /// <param name="_age">나이</param>
        /// <param name="_ageChk">30세 이상 나이 체크</param>
        /// <param name="_tall">키</param>
        /// <param name="_sexType">성별</param>
        public CommonData(DateTime _createDate, DateTime _modifyDate, string _id, string _pw, float _tall, byte _age, bool _ageChk, SexTypes _sexType)
        {
            this.createDate = _createDate;
            this.modifyDate = _modifyDate;
            this.id = _id;
            this.pw = _pw;
            this.age = _age;
            this.ageChk = _ageChk;
            this.tall = _tall;
            this.sexType = _sexType;

        }
    }
}