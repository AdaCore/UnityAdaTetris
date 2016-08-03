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
