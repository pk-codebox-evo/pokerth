﻿/***************************************************************************
 *   Copyright (C) 2008 by Lothar May                                      *
 *                                                                         *
 *   This file is part of pokerth_console.                                 *
 *   pokerth_console is free software: you can redistribute it and/or      *
 *   modify it under the terms of the GNU Affero General Public License    *
 *   as published by the Free Software Foundation, either version 3 of     *
 *   the License, or (at your option) any later version.                   *
 *                                                                         *
 *   pokerth_console is distributed in the hope that it will be useful,    *
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of        *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
 *   GNU General Public License for more details.                          *
 *                                                                         *
 *   You should have received a copy of the                                *
 *   GNU Affero General Public License along with pokerth_console.         *
 *   If not, see <http://www.gnu.org/licenses/>.                           *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace pokerth_console
{
	class GameInfo : IdObject
	{
		public enum Mode
		{
			Created = 1,
			Started,
			Closed
		}

		public GameInfo(uint id, string name, Mode mode, List<uint> playerSlots, uint startMoney)
			: base(id, name)
		{
			m_mutex = new Object();
			m_mode = mode;
			m_playerSlots = playerSlots;
			m_startMoney = startMoney;
		}

		public List<uint> PlayerSlots
		{
			get
			{
				lock (m_playerSlots)
				{
					// returns a copy(!)
					return new List<uint>(m_playerSlots);
				}
			}
			set
			{
				lock (m_playerSlots)
				{
					m_playerSlots = value;
				}
			}
		}

		public Mode CurrentMode
		{
			get
			{
				lock (m_mutex)
				{
					return m_mode;
				}
			}
			set
			{
				lock (m_mutex)
				{
					m_mode = value;
				}
			}
		}

		public uint StartMoney
		{
			get
			{
				lock (m_mutex)
				{
					return m_startMoney;
				}
			}
		}

		private Object m_mutex;
		private Mode m_mode;
		private List<uint> m_playerSlots;
		private uint m_startMoney;
	}
}
