﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodShoppingWEBUI.Core.DTO;

namespace FoodShoppingWEBUI.Helper.SessionHelper
{
	public class SessionManager
	{

		public static LoginDTO? LoggedUser
		{
			get => AppHttpContext.Current.Session.GetObject<LoginDTO>("LoginDTO");
			set => AppHttpContext.Current.Session.SetObject("LoginDTO", value);

		}

	}
}
