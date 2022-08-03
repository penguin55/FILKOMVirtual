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
        - calculate == "1-1":
            -> ActiveR1
        - calculate == "1-2":
            -> ClearR1
        - calculate == "2-1":
            -> ActiveR2
        - calculate == "2-2":
            -> ClearR2
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

=== ActiveR1 ===
    - ???_{npc_role_m}: Hai! Selamat datang di kampus virtual tercinta, kampus virtual Universitas Brawijaya!
    - {player_name}: Halo kak!
    - ???_{npc_role_m}: Perkenalkan namaku {npc_name} dari divisi {npc_role}. Panggil saja {npc_nick} untuk lebih singkatnya.
    - {player_name}: Hai kak {npc_nick}! Nama saya {player_name}, salam kenal.
    - {npc_nick}_{npc_role_m}: Salam kenal, {player_name}! Terima kasih sudah datang untuk mengikuti Rangkaian 1. Jangan lupa untuk menyelesaikan semua penugasan pada rangkaian 1. Selamat mengerjakan!
    - {player_name}: Siap kak! {Notify("clear")}
-> END

=== ClearR1 ===
    - {npc_nick}_{npc_role_m}: Hai! Ada apa? Apakah kamu sudah berbicara pada Ketua Pelaksana di depan gedung F?
    - {npc_nick}_{npc_role_m}: Jangan lupa selesaikan semua penugasan yang ada. Semangat!!!
-> END



=== ActiveR2 ===
    - {npc_nick}_{npc_role_m}: Hai! Selamat datang kembali di kampus virtual tercinta, kampus virtual Universitas Brawijaya! Saat ini rangkaian 2 FILKOM Virtual telah dimulai.
    - {player_name}: Selamat pagi kak!
    - {npc_nick}_{npc_role_m}: Untuk penugasan rangkaian 2 bisa langsung mendatangi panitia pendamping ya dek di lapangan parkir!.
    - {player_name}: Baik kak {npc_nick}!
    - {npc_nick}_{npc_role_m}: Siap! Bagus!
    - {player_name}: Izin meninggalkan tempat kak! {Notify("clear")}
-> END

=== ClearR2 ===
    - {npc_nick}_{npc_role_m}: Hai! Ada apa? Apakah kamu sudah berbicara pada Pendamping yang ada di lapangan parkir?
    - {npc_nick}_{npc_role_m}: Jangan lupa selesaikan semua penugasan yang ada. Semangat!!!
-> END



=== ActiveR3 ===
    - {npc_nick}_{npc_role_m}: Halooo!! Wah rajin juga ya kamu. Ini sudah rangkaian terakhir nih. Akan ada open booth yang bisa kamu kunjungi untuk mencari informasi mengenai FILKOM. Jangan lupa datangi Ketua Pelaksana dulu ya di depan Gedung F FILKOM Virtual!
    - {player_name}: Siap kak, laksanakan!
    - {npc_nick}_{npc_role_m}: Yap bagus, semangatmu bagus sekali, jangan sampai hilang!!!
    - {player_name}: Terima kasih kak! {Notify("clear")}
-> END

=== ClearR3 ===
    - {npc_nick}_{npc_role_m}: Hai! Ada apa? Apakah kamu sudah berbicara pada Ketua Pelaksana di depan gedung F?
    - {npc_nick}_{npc_role_m}: Jangan lupa selesaikan semua penugasan yang ada. Semangat!!!
-> END

