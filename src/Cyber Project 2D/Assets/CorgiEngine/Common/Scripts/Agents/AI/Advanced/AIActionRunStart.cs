﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
	/// <summary>
	/// This action will cause your AI character to start running
	/// </summary>
	[AddComponentMenu("Corgi Engine/Character/AI/Actions/AI Action Run Start")]
	public class AIActionRunStart : AIAction
	{
		protected CharacterRun _characterRun;

		/// <summary>
		/// On init we grab our CharacterRun component
		/// </summary>
		public override void Initialization()
		{
			_characterRun = this.gameObject.GetComponentInParent<Character>()?.FindAbility<CharacterRun>();
		}

		/// <summary>
		/// On PerformAction we start running
		/// </summary>
		public override void PerformAction()
		{
			_characterRun.RunStart();
		}
	}
}