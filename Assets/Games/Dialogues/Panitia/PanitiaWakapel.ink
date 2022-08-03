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
    - ???_{npc_role_m}: Oh, halo! Jangan lupa ikuti arahan penugasan ya dek.
    - {player_name}: Iya kak!
-> END

=== ActiveR1 ===
    - ???_{npc_role_m}: Oh halo! Sudah bertemu Ketua Pelaksana ya dek?
    - {player_name}: Halo kak! Iya kak, sudah. Saya diberikan arahan untuk menemui kakak selaku {npc_role}.
    - ???_{npc_role_m}: Oh baik, sebelumnya perkenalkan aku {npc_name} bisa dipanggil dengan {npc_nick} disini menjabat sebagai {npc_role}. Namamu siapa nih?
    - {player_name}: Nama saya {player_name}, salam kenal kak.
    - {npc_nick}_{npc_role_m}: Halo {player_name}! Selamat datang di FILKOM Virtual. Sudah diberitahu oleh Ketua Pelaksana mengenai apa itu FILKOM Virtual kan?
    - {player_name}: Sudah kak!
    - {npc_nick}_{npc_role_m}: Baik, disini aku cuma mau nambahkan saja. Rangkaian nantinya akan terdiri dari 3 rangkaian dengan beberapa penugasan di setiap rangkaiannya, jadi pastikan kamu mengerjakan semua penugasannya tepat waktu ya!
    - {npc_nick}_{npc_role_m}: Untuk tugas pertamamu coba datangi panitia Pendamping di dekat lapangan luas disebelah kiri gedung F! Disana kamu akan diberikan penugasan lebih lengkap pada rangkaian 1 ini. 
    - {player_name}: Siap kak! Saya laksanakan!
    - {npc_nick}_{npc_role_m}: Oh iya, jangan lupa ya perkenalan di setiap kakak-kakak yang kamu temui. Karena tak kenal maka tak sayang.
    - {player_name}: Baik kak! {Notify("clear")}
-> END

=== ClearR1 ===
    - {npc_nick}_{npc_role_m}: Sudah bertemu dan berkenalan dengan panitia pendamping? Apakah tugas yang diberikan terlalu berat?
    - {npc_nick}_{npc_role_m}: Walau seberat apapun jangan diabaikan ya!
-> END



=== NotActiveR2 ===
    - {npc_nick}_{npc_role_m}: Selamat! Kamu sudah memasuki rangkaian 2 FILKOM Virtual. Jangan lupa menyelesaiakan penugasan yang diberikan ya.
    - {player_name}: Siap kak!
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
    - {npc_nick}_{npc_role_m}: Wah tidak terasa ya, sudah rangkaian akhir kita bertemu. Sudah coba untuk berbicara ke panitia Disma?
    - {player_name}: Belum kak!
    - {npc_nick}_{npc_role_m}: Coba tanya ke panitia Disma dulu ya!
    - {player_name}: Baik kak!
-> END

=== ActiveR3 ===
    - {npc_nick}_{npc_role_m}: Rangkaian akhir bukanlah akhir dari segalanya. Akan ada beberapa penugasan menarik buatmu nih pada rangkaian ini.
    - {player_name}: Wah apa itu kak?
    - {npc_nick}_{npc_role_m}: Pada rangkaian ini kamu memasuki rangkaian Open House. Dimana rangkaian ini akan berisi penugasan untuk mencari informasi dari kelembagaan dan komunitas yang ada di FILKOM, kamu juga dapat memotret momen-momen bagus di FILKOM Virtual.
    - {player_name}: Wah sepertinya asik nih! 
    - {npc_nick}_{npc_role_m}: Jangan lupa untuk selesaiakan semua penugasan pada rangkaian 3 ini ya! Setelah ini datangi lapangan parkir untuk memasuki booth open house, atau kamu juga bisa melihat pada map yang disediakan.
    - {player_name}: Iya kak! {Notify("clear")}
-> END

=== ClearR3 ===
    - {npc_nick}_{npc_role_m}: Sudah mengenal semua kelembagaan dan komunitas di FILKOM?
    - {npc_nick}_{npc_role_m}: Banyak informasi yang dapat kamu ambil nih sebelum ingin memasuki kelembagaan atau komunitas di FILKOM.
-> END