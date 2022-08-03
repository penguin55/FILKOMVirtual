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
    - ???_{npc_role_m}: Halooo! Selamat datang di FILKOM Virtual. Kamu mahasiswa baru ya?
    - {player_name}: Iya kak!
    - ???_{npc_role_m}: Jangan lupa untuk berbicara ke panitia pendamping 1 dulu ya dek untuk arahan selanjutnya.
-> END

=== ActiveR1 ===
    - {player_name}: Halo kak! Perkenalkan nama saya {player_name}, saya sebagai mahasiswa baru FILKOM. Saya sudah berbicara pada panitia Pendamping 1 untuk penugasan yang akan dilakukan.
    - ???_{npc_role_m}: Halo, perkenalkan juga, saya {npc_nick} sebagai {npc_role} pada acara PK2Maba. Sudah kamu selesaiakan semua penugasannya? Jangan lupa untuk selesaiakan ya apabila belum. Ini adalah rangkaian pertama dimana kamu akan mendapatkan informasi umum mengenai gedung-gedung FILKOM.
    - {player_name}: Baik kak, dimengerti!
    - {npc_nick}_{npc_role_m}: Semangat ya! 
    - {player_name}: Iya kak, terima kasih untuk semangatnya. {Notify("clear")}
-> END

=== ClearR1 ===
    - {npc_nick}_{npc_role_m}: Ditunggu untuk rangkaian selanjutnya ya!
    - {npc_nick}_{npc_role_m}: Akan ada banyak penugasan menarik untukmu.
-> END



=== NotActiveR2 ===
    - {npc_nick}_{npc_role_m}: Hai! Kita bertemu lagi di rangkaian 2. Sudah mencoba berbicara ke panitia Disma untuk arahan awal?
-> END

=== ActiveR2 ===
    - {npc_nick}_{npc_role_m}: Apakah sudah kamu selesaiakan untuk penugasan yang diberikan?
    +   [Sudah]
    +   [Belum]
    - {npc_nick}_{npc_role_m}: Rangkaian ini merupakan rangkaian Share Your Moment, tidak hanya dalam penugasan saja kamu bisa lakukan foto, kamu juga bisa memotret setiap saat saat kamu menginginkannya.
    - {player_name}: Iya kak, siap! {Notify("clear")}
-> END

=== ClearR2 ===
    - {npc_nick}_{npc_role_m}: Tidak mau memotret panitia? Hehehe... Kamu bisa gunakan kameramu untuk memotret momen penting juga loh!
-> END


=== NotActiveR3 ===
    - {npc_nick}_{npc_role_m}: Jangan lupa datangi Wakil Ketua Pelaksana dulu ya!
-> END

=== ActiveR3 ===
    - {npc_nick}_{npc_role_m}: Wah, informasi apa saja nih yang sudah kamu dapatkan?
    - {player_name}: Informasi tentang kelembagaan dan komunitas kak!
    - {npc_nick}_{npc_role_m}: Apakah kamu sudah memutuskan untuk join kelembagaan atau komunitas di FILKOM?
    +   [Sudah kak!]
    +   [Masih saya pertimbangkan lagi.]
    - {npc_nick}_{npc_role_m}: Tidak perlu terburu, kamu bisa pertimbangkan lagi.  {Notify("clear")}
-> END

=== ClearR3 ===
    - {npc_nick}_{npc_role_m}: Setelah mendapatkan informasi kelembagaan dan komunitas, jangan lupa untuk pertimbangkan dan sesuaikan passionmu ya untuk bergabung di kelembagaan maupun komunitas.  
    - {npc_nick}_{npc_role_m}: Jangan asal ikut karena terpaksa dan ikut-ikutan. Karena kamu akan berkembang lebih bagus apabila kamu ikut karena passionmu dan kamu menyukainya.
    - {player_name}: Baik kak, terima kasih atas sarannya!
-> END