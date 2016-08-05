------------------------------------------------------------------------------
--                                                                          --
--                             GNAT EXAMPLE                                 --
--                                                                          --
--                      Copyright (C) 2016, AdaCore                         --
--                                                                          --
-- GNAT is free software;  you can  redistribute it  and/or modify it under --
-- terms of the  GNU General Public License as published  by the Free Soft- --
-- ware  Foundation;  either version 3,  or (at your option) any later ver- --
-- sion.  GNAT is distributed in the hope that it will be useful, but WITH- --
-- OUT ANY WARRANTY;  without even the  implied warranty of MERCHANTABILITY --
-- or FITNESS FOR A PARTICULAR PURPOSE.                                     --
--                                                                          --
-- As a special exception under Section 7 of GPL version 3, you are granted --
-- additional permissions described in the GCC Runtime Library Exception,   --
-- version 3.1, as published by the Free Software Foundation.               --
--                                                                          --
-- You should have received a copy of the GNU General Public License and    --
-- a copy of the GCC Runtime Library Exception along with this program;     --
-- see the files COPYING3 and COPYING.RUNTIME respectively.  If not, see    --
-- <http://www.gnu.org/licenses/>.                                          --
--                                                                          --
-- GNAT was originally developed  by the GNAT team at  New York University. --
-- Extensive contributions were provided by Ada Core Technologies Inc.      --
--                                                                          --
------------------------------------------------------------------------------

with Tetris; use Tetris;

package Game_Loop is

   type Cycle_State is (Init, New_Piece, Falling, Lost);

   function Get_State return Cycle_State
     with Export,
     Convention => C,
     External_Name => "tetris_get_state";

   procedure Cycle
     with Export,
     Convention => C,
     External_Name => "tetris_cycle";

   function Get_Kind (X, Y : Integer) return Cell
     with Export,
     Convention => C,
     External_Name => "tetris_get_kind";

   function Get_Cur_Piece return Piece
     with Export,
     Convention => C,
     External_Name => "tetris_get_cur_piece";

   type CSharp_Bool is new Boolean with Size => 8;

   function Check_Possible_I_Shapes
     (D : Direction; X, Y : Integer) return CSharp_Bool
     with Export,
     Convention => C,
     External_Name => "tetris_check_possible_i_shapes";

   function Check_Possible_Three_Shapes
     (S : Shape; D : Direction; X, Y : Integer) return CSharp_Bool
     with Export,
     Convention => C,
     External_Name => "tetris_check_possible_three_shapes";

   function Do_Action (A : Action) return CSharp_Bool
     with Export,
     Convention => C,
     External_Name => "tetris_do_action";

   function Is_Line_Destroyed (Y : Integer) return CSharp_Bool
        with Export,
     Convention => C,
     External_Name => "tetris_is_line_destroyed";

end Game_Loop;
