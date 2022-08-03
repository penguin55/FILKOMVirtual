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
VAR booth_name = "K-Risma"
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
    {
        - questStatus == 0:
            -> NotActive
        - questStatus == 1:
            -> Active
        - questStatus == 2:
            -> Clear
    }
-> END

=== NotActive ===
    - ???_{npc_role_m}: Halo, selamat datang di booth {booth_name}. Apakah kamu sudah ditugaskan untuk memulai penugasan?
    - {player_name}: Belum kak!
    - ???_{npc_role_m}: Ikuti alur penugasannya dulu ya dek. Nanti bisa datangi booth {booth_name} lagi.
    - {player_name}: Ok kak!
-> END

=== Active ===
    - {player_name}: Halo kak! Saya {player_name} ingin mengetahui {booth_name} lebih dalam.
    - ???_{npc_role_m}: Halo {player_name}. Namaku {npc_nick} sebagai {npc_role}. Baik, aku jelaskan secara singkat ya, mohon disimak!
    - -> Extra
-> END

=== Clear ===
    - {npc_nick}_{npc_role_m}: Hai! Apa kamu sudah meyakinkan diri untuk mendaftar ke {booth_name}?
    - {player_name}: Masih belum kak.
    - {npc_nick}_{npc_role_m}: Apakah perlu dijelaskan kembali mengenai {booth_name}?
    +   [Iya kak, saya butuh]
        {npc_nick}_{npc_role_m}: Ok, aku jelaskan lagi ya. Simak dengan baik.
        -> ExtraClear
    +   [Tidak kak, sudah jelas]
        {npc_nick}_{npc_role_m}: Mantap! Jangan lupa buat mendaftar ya!
    - {npc_nick}_{npc_role_m}: Tidak perlu terburu-buru untuk memilih!
-> END

=== Extra ===
    - {npc_nick}_{npc_role_m}: Kelompok Riset Mahasiswa (K-RISMA) adalah salah satu Lembaga Semi Otonom di Fakultas Ilmu Komputer, Universitas Brawijaya yang bergerak dalam bidang riset dan penalaran. K-RISMA berdiri pada tanggal 12 Desember 2012.
    - {npc_nick}_{npc_role_m}: K-RISMATIIK adalah julukan K-RISMA saat pertama kali terbentuk (K-RISMA, 2012). Pada 29 Januari 2015, terbitnya SK Rektor Universitas Brawijaya Nomor 49/2015 yang secara resmi mengubah PTIIK menjadi FILKOM (FILKOM UB, 2016), membuat K-RISMATIIK berganti menjadi K-RISMA FILKOM UB.
    - {npc_nick}_{npc_role_m}: K-RISMA memiliki dua tujuan yang menjadi dasar latar belakang pembentukannya (K-RISMA, 2012). Tujuan pertama, K-RISMA berperan dalam membantu mahasiswa yang memiliki ide dalam pembuatan teknologi, untuk dapat menuangkannya dalam bentuk tulisan. 
    - {npc_nick}_{npc_role_m}: Tulisan merupakan salah satu faktor penting ketika mahasiswa hendak mengikutsertakan ide, karya, atau pun teknologi yang mereka buat kedalam suatu kompetisi.
    - {npc_nick}_{npc_role_m}: Tujuan kedua, K-RISMA berperan sebagai wadah yang berfungsi sebagai penampung dan pendistribusi informasi kompetisi. Oleh karena itu, K-RISMA terbentuk demi terus membangkitkan jiwa kepenulisan, penalaran, kerisetan, serta prestasi Mahasiswa FILKOM.
    - {npc_nick}_{npc_role_m}: Itu semua penjelasan singkat mengenai {booth_name}, apa sudah jelas?
    +   [Sudah Kak]
        {npc_nick}_{npc_role_m}: Bagus! Jangan lupa untuk daftar ke {booth_name} ya!
        {player_name}: Hehehe, siap kak saya pertimbangkan dulu.
    +   [Belum Kak]
        {npc_nick}_{npc_role_m}: Ok, aku jelaskan lagi ya. Simak dengan baik.
        -> Extra
    - {npc_nick}_{npc_role_m}: Mantap! Pertimbangkan dengan matang ya, kami siap menerima kamu kapanpun.
    - {player_name}: Baik kak! Terima kasih banyak
    - {npc_nick}_{npc_role_m}: Sama-sama! {Notify("clear")}
-> END

=== ExtraClear ===
    - {npc_nick}_{npc_role_m}: Kelompok Riset Mahasiswa (K-RISMA) adalah salah satu Lembaga Semi Otonom di Fakultas Ilmu Komputer, Universitas Brawijaya yang bergerak dalam bidang riset dan penalaran. K-RISMA berdiri pada tanggal 12 Desember 2012.
    - {npc_nick}_{npc_role_m}: K-RISMATIIK adalah julukan K-RISMA saat pertama kali terbentuk (K-RISMA, 2012). Pada 29 Januari 2015, terbitnya SK Rektor Universitas Brawijaya Nomor 49/2015 yang secara resmi mengubah PTIIK menjadi FILKOM (FILKOM UB, 2016), membuat K-RISMATIIK berganti menjadi K-RISMA FILKOM UB.
    - {npc_nick}_{npc_role_m}: K-RISMA memiliki dua tujuan yang menjadi dasar latar belakang pembentukannya (K-RISMA, 2012). Tujuan pertama, K-RISMA berperan dalam membantu mahasiswa yang memiliki ide dalam pembuatan teknologi, untuk dapat menuangkannya dalam bentuk tulisan. 
    - {npc_nick}_{npc_role_m}: Tulisan merupakan salah satu faktor penting ketika mahasiswa hendak mengikutsertakan ide, karya, atau pun teknologi yang mereka buat kedalam suatu kompetisi.
    - {npc_nick}_{npc_role_m}: Tujuan kedua, K-RISMA berperan sebagai wadah yang berfungsi sebagai penampung dan pendistribusi informasi kompetisi. Oleh karena itu, K-RISMA terbentuk demi terus membangkitkan jiwa kepenulisan, penalaran, kerisetan, serta prestasi Mahasiswa FILKOM.
    - {npc_nick}_{npc_role_m}: Itu semua penjelasan singkat mengenai {booth_name}, apa sudah jelas?
    +   [Sudah Kak]
        {npc_nick}_{npc_role_m}: Bagus! Jangan lupa  daftar ke {booth_name} ya!
        {player_name}: Hehehe, siap kak saya pertimbangkan dulu.
    +   [Belum Kak]
        {npc_nick}_{npc_role_m}: Ok, aku jelaskan lagi ya. Simak dengan baik.
        -> Extra
    - {npc_nick}_{npc_role_m}: Mantap! Pertimbangkan dengan matang ya, kami siap menerima kamu kapanpun.
-> END
