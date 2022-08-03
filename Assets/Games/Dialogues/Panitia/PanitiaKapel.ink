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
    - ???_{npc_role_m}: Hai! Apakah kamu tersesat?
    - ???_{npc_role_m}: Oh kamu mahasiswa baru FILKOM?
    - {player_name}: Iya kak!
    - ???_{npc_role_m}: Wah, halooo! Selamat datang! Apakah kamu sudah diberikan arahan oleh panitia Disma didepan pintu masuk?
    - ???_{npc_role_m}: Hehehe, jangan malu dek. Malu bertanya sesat dijalan.
-> END

=== ActiveR1 ===
    - ???_{npc_role_m}: Oh halo! Sudah bertemu panitia Disma didepan ya dek?
    - {player_name}: Halo kak! Iya kak, sudah diberikan arahan untuk mengerjakan penugasan pada rangkaian 1 dengan menemui kakak selaku {npc_role}.
    - ???_{npc_role_m}: Oh baik, sebelumnya perkenalkan aku {npc_name} bisa dipanggil dengan {npc_nick} disini menjabat sebagai {npc_role}. Namamu siapa nih?
    - {player_name}: Nama saya {player_name}, salam kenal kak.
    - {npc_nick}_{npc_role_m}: Halo {player_name}! Selamat datang di FILKOM Virtual. FILKOM Virtual merupakan gim simulasi PK2Maba yang dirancang untuk menambahkan penugasan pada rangkaian PK2Maba. 
    - {npc_nick}_{npc_role_m}: Penugasan yang diberikan diharapkan mahasiswa baru dapat mengenal FILKOM lebih jauh lagi. Sebelum memulai penugasan alangkah baiknya mengenal dulu, karena tak kenal maka tak sayang.
    - {npc_nick}_{npc_role_m}: Coba datangi Wakil Ketua Pelaksana dahulu ya!
    - {player_name}: Siap kak! Saya laksanakan!
    - {npc_nick}_{npc_role_m}: Oh iya, jangan lupa ya perkenalan. Karena tak kenal maka tak sayang.
    - {player_name}: Baik kak! {Notify("clear")}
-> END

=== ClearR1 ===
    - {npc_nick}_{npc_role_m}: Sudah bertemu dan berkenalan dengan Wakil Ketua Pelaksana? 
    - {npc_nick}_{npc_role_m}: Ingat ya, tak kenal maka tak sayang!
-> END

 
 
 === NotActiveR2 ===
    - {npc_nick}_{npc_role_m}: Hai! Bagaiamana harimu?
    - {player_name}: Baik-baik saja kak.
    - {npc_nick}_{npc_role_m}: Saat ini rangkaian 2 sudah dimulai, apa kamu sudah mencoba tanya ke Panitia Disma untuk dapat arahan?
    - {player_name}: Belum kak!
    - {npc_nick}_{npc_role_m}: Kalau gitu coba tanya ke panitia Disma dulu ya dek agar dapat arahan untuk penugasan.
    - {npc_nick}_{npc_role_m}: Hehehe, jangan malu dek. Malu bertanya sesat dijalan.
    - {player_name}: Siap kak! {Notify("")}
-> END

=== ActiveR2 ===
    - {npc_nick}_{npc_role_m}: Template
    - {player_name}: Template!
    - {npc_nick}_{npc_role_m}: Template!
    - {player_name}: Template! {Notify("clear")}
-> END

=== ClearR2 ===
    - {npc_nick}_{npc_role_m}: Template
    - {player_name}: Template!
    - {npc_nick}_{npc_role_m}: Template!
    - {player_name}: Template!
-> END



=== NotActiveR3 ===
    - {npc_nick}_{npc_role_m}: Halo! Selamat ya, sudah sampai di rangkaian akhir FILKOM Virtual! Apakah kamu sudah menyelesaikan penugasan hari ini?
    - {player_name}: Belum kak!
    - {npc_nick}_{npc_role_m}: Segera selesaikan ya!
-> END

=== ActiveR3 ===
    - {npc_nick}_{npc_role_m}: Oh Hai! Hebat!!
    - {npc_nick}_{npc_role_m}: Kamu telah menyelesaikan penugasan yang ada. Tapi, apa sudah semuanya? Jangan lupa achievementnya dikumpulkan juga ya!
    - {npc_nick}_{npc_role_m}: Terima kasih telah memainkan FILKOM Virtual hingga rangkaian akhir. Saya selaku {npc_role} ingin mengucapkan rasa Terima kasih sebesar-besarnya kepadamu! Jangan sampai melupakan informasi yang didapat pada FILKOM Virtual ya.
    - {player_name}: Iya kak! Gak saya lupakan kok. {Notify("clear")}
-> END

=== ClearR3 ===
    - {npc_nick}_{npc_role_m}: Terima kasih sudah memainkan FILKOM Virtual!
    - {npc_nick}_{npc_role_m}: jika kamu ingin memainkan gim FILKOM Virtual diluar jadwal rangkaian, kamu masih bisa kok hanya saja tidak akan ada penugasan lagi! Tetap stay tune ya untuk update selanjutnya!
-> END