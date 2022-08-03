EXTERNAL GetRangkaian()
EXTERNAL GetPlayerName()
EXTERNAL GetNPCName()
EXTERNAL GetNPCNickname()
EXTERNAL GetNPCRole()
EXTERNAL Notify(x)
EXTERNAL GetQuestStatus()

VAR rangkaian = "rangkaian_0"
VAR player_name = "Tom"
VAR npc_name = "Ella"
VAR npc_nick = "Margaret"
VAR npc_role = "Korlap"
VAR npc_role_m = ""
VAR questStatus = -1
VAR calculate = ""
~ npc_role_m = "<b>({npc_role})</b>"


-> Init

=== Init ===
    ~ rangkaian = GetRangkaian()
    ~ player_name = GetPlayerName()
    ~ npc_nick = GetNPCNickname()
    ~ npc_name = GetNPCName()
    ~ npc_role = GetNPCRole()
    ~ npc_role_m = "<b>({npc_role})</b>"
    ~ questStatus = GetQuestStatus()
    ~ calculate = rangkaian + "-" + questStatus
    {
        - calculate == "1-0":
            -> NotActiveR1
        - calculate == "1-1":
            -> ActiveR1
        - calculate == "1-2":
            -> ClearR1
        - calculate == "2-0":
            -> NotActiveR2
        - calculate == "2-1":
            -> ActiveR2
        - calculate == "2-2":
            -> ClearR2
        - calculate == "3-0":
            -> NotActiveR3
        - calculate == "3-1":
            -> ActiveR3
        - calculate == "3-2":
            -> ClearR3
        - else:
            -> NONE
    }
-> END

=== NONE ===
    - {npc_nick}_{npc_role_m}: Wah maaf sekali, rangkaian pada FILKOM Virtual telah selesai. Tapi jangan bersedih hati dulu, masih ada informasi menarik yang bisa ditelusuri di gim ini. Jangan sungkan untuk explorasi!
    - {npc_nick}_{npc_role_m}: Informasi lebih lanjut datangi official account kami ya!
-> END

=== NotActiveR1 ===
    - ???_{npc_role_m}: ...
    - ???_{npc_role_m}: Halo dek! Jangan lupa datangi Ketua Pelaksana dulu ya, baru datang kesini lagi untuk memulain penugasan.
-> END

=== ActiveR1 ===
    - {player_name}: Halo kak! Perkenalkan nama saya {player_name}, saya sebagai mahasiswa baru FILKOM siap untuk menerima penugasan selanjutnya.
    - ???_{npc_role_m}: Hai, perkenalkan juga, saya {npc_nick} sebagai {npc_role} pada acara PK2Maba. Berikut ada beberapa penugasan yang harus kamu kerjakan, bisa kamu cek dibagian list penugasan. Apabila bingung bisa dilihat pada menu tutorial ya!
    - {player_name}: Baik kak, dimengerti! Akan saya kerjakan penugasan yang telah diberikan.
    - {npc_nick}_{npc_role_m}: Semangat ya! 
    - {player_name}: Iya kak, terima kasih untuk semangatnya. {Notify("clear")}
-> END

=== ClearR1 ===
    - {npc_nick}_{npc_role_m}: Apakah sudah kamu selesaikan penugasan yang telah diberikan tadi?
    - {npc_nick}_{npc_role_m}: Jangan ditunda ya! Jangan sampai lewat deadline biar tidak berat hehehe! Semangat!!!
-> END


 
 === NotActiveR2 ===
    - {npc_nick}_{npc_role_m}: Hai {player_name}! Kita berada di rangkaian 2 nih, rangkaian ini bernama Share Your Moments dimana kamu akan memotret momen-momen poenting di FILKOM Virtual.
    - {npc_nick}_{npc_role_m}: Sebelumnya coba datangi panitia Disma dulu ya dek, untuk diberikan arahan dan informasi.
    - {player_name}: Baik kak!
-> END

=== ActiveR2 ===
    - {npc_nick}_{npc_role_m}: Sudah dapat arahan dari panitia Disma dek?
    - {player_name}: Sudah kak!
    - {npc_nick}_{npc_role_m}: Bagus! Ini aku berikan daftar penugasan yang bisa kamu kerjakan, kamu akan memotret gedung-gedung FILKOM di FILKOM Virtual. Jangan lupa untuk kerjakan setiap penugasan ya!
    - {player_name}: Oke kak! {Notify("clear")}
-> END

=== ClearR2 ===
    - {npc_nick}_{npc_role_m}: Sudah memotret semua gedung?
    - {npc_nick}_{npc_role_m}: Jangan lupa selesaiakan semua penugasanmu.
-> END



=== NotActiveR3 ===
    - {npc_nick}_{npc_role_m}: Wah tidak terasa ya, kita sudah memasuki rangkaian terakhir. Jangan lupa untuk selesaikan semua penugasanmu ya!
    - {player_name}: Iya kak!
-> END

=== ActiveR3 ===
    - {npc_nick}_{npc_role_m}: Template
    - {player_name}: Template!
    - {npc_nick}_{npc_role_m}: Template!
    - {player_name}: Template! {Notify("clear")}
-> END

=== ClearR3 ===
    - {npc_nick}_{npc_role_m}: Template
    - {player_name}: Template!
    - {npc_nick}_{npc_role_m}: Template!
    - {player_name}: Template!
-> END