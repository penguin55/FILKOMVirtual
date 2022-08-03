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
VAR booth_name = "Robotiik"
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
    - {npc_nick}_{npc_role_m}: Robotiik merupakan komunitas yang mewadahi mahasiswa FILKOM untuk mengembangkan kreativitas, inovatif, penelitian, lomba dan pengenalan kepada masyarakat dalam bidang robotika.
    - {npc_nick}_{npc_role_m}: Seorang engineer Robotiik harus memiliki jiwa yang semangat, unggul dan sungguh-sungguh dalam mengerjakan robot.
    - {npc_nick}_{npc_role_m}: Engineer ini juga bertugas untuk menciptakan dan mengerjakan robot, yang nantinya akan dilombakan.
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
    - {npc_nick}_{npc_role_m}: Robotiik merupakan komunitas yang mewadahi mahasiswa FILKOM untuk mengembangkan kreativitas, inovatif, penelitian, lomba dan pengenalan kepada masyarakat dalam bidang robotika.
    - {npc_nick}_{npc_role_m}: Seorang engineer Robotiik harus memiliki jiwa yang semangat, unggul dan sungguh-sungguh dalam mengerjakan robot.
    - {npc_nick}_{npc_role_m}: Engineer ini juga bertugas untuk menciptakan dan mengerjakan robot, yang nantinya akan dilombakan.
    - {npc_nick}_{npc_role_m}: Itu semua penjelasan singkat mengenai {booth_name}, apa sudah jelas?
    +   [Sudah Kak]
        {npc_nick}_{npc_role_m}: Bagus! Jangan lupa  daftar ke {booth_name} ya!
        {player_name}: Hehehe, siap kak saya pertimbangkan dulu.
    +   [Belum Kak]
        {npc_nick}_{npc_role_m}: Ok, aku jelaskan lagi ya. Simak dengan baik.
        -> Extra
    - {npc_nick}_{npc_role_m}: Mantap! Pertimbangkan dengan matang ya, kami siap menerima kamu kapanpun.
-> END
