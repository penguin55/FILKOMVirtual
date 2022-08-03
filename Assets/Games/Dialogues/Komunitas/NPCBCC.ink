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
VAR booth_name = "BCC"
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
    - {npc_nick}_{npc_role_m}: Basic Computing Community (BCC) adalah komunitas mahasiswa FILKOM Universitas Brawijaya yang tertarik untuk mengembangkan, menggali, dan berkolaborasi dalam menghasilkan produk digital yang bermanfaat dan berdampak.
    - {npc_nick}_{npc_role_m}: BCC memiliki empat Departemen, yaitu Front End, Back End, Product, dan Computational Intelligence. Tiap Departemen memiliki peran dan bidang keahlian masing masing terutama dalam mempersiapkan anggotanya menuju dunia kerja
    - {npc_nick}_{npc_role_m}: Departemen Computational Intelligence memiliki 2 subdepartemen yaitu Competitive Programming dan Data Science. Dimana departemen ini bergerak pada analisa data dan memecahkan solusi.
    - {npc_nick}_{npc_role_m}: Departemen Back-End merupakan salah satu departemen yang memiliki fokus dalam pengembangan produk di sisi server dengan service yang bertugas mengelola data dan keamanan server.
    - {npc_nick}_{npc_role_m}: Departemen Product adalah departemen yang berfokus pada pengembangan dan pengilustrasian ide produk digital yang bermanfaat. Departemen ini dibagi menjadi dua subdepartemen yaitu Product Management dan Product Design.
    - {npc_nick}_{npc_role_m}: Departemen Front-End merupakan departemen yang mempelajari cara mengimplementasikan design interface ke dalam bentuk 'aplikasi baik website maupun mobile.
    - {npc_nick}_{npc_role_m}: Selain 4 departemen, BCC juga terdapat 3 divisi yaitu Research and Development, Public Relation, dan Talent Development.
    - {npc_nick}_{npc_role_m}: Divisi Research and Development merupakan divisi yang bertanggung jawab dalam pengembangan riset yang dilakukan BCC ke mahasiswa UB maupun FILKOM.
    - {npc_nick}_{npc_role_m}: Divisi Public Relation adalah divisi yang memiliki peran untuk memastikan segala informasi dapat terkomunikasikan dengan baik antar anggota internal maupun external BCC.
    - {npc_nick}_{npc_role_m}: Terakhir terdapat divisi Talent Development dimana divisi ini yang mengatur dan membina anggota internal. Divisi ini juga menjadi pengawas bagi tiap departemen dalam BCC.
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
    - {npc_nick}_{npc_role_m}: Basic Computing Community (BCC) adalah komunitas mahasiswa FILKOM Universitas Brawijaya yang tertarik untuk mengembangkan, menggali, dan berkolaborasi dalam menghasilkan produk digital yang bermanfaat dan berdampak.
    - {npc_nick}_{npc_role_m}: BCC memiliki empat Departemen, yaitu Front End, Back End, Product, dan Computational Intelligence. Tiap Departemen memiliki peran dan bidang keahlian masing masing terutama dalam mempersiapkan anggotanya menuju dunia kerja
    - {npc_nick}_{npc_role_m}: Departemen Computational Intelligence memiliki 2 subdepartemen yaitu Competitive Programming dan Data Science. Dimana departemen ini bergerak pada analisa data dan memecahkan solusi.
    - {npc_nick}_{npc_role_m}: Departemen Back-End merupakan salah satu departemen yang memiliki fokus dalam pengembangan produk di sisi server dengan service yang bertugas mengelola data dan keamanan server.
    - {npc_nick}_{npc_role_m}: Departemen Product adalah departemen yang berfokus pada pengembangan dan pengilustrasian ide produk digital yang bermanfaat. Departemen ini dibagi menjadi dua subdepartemen yaitu Product Management dan Product Design.
    - {npc_nick}_{npc_role_m}: Departemen Front-End merupakan departemen yang mempelajari cara mengimplementasikan design interface ke dalam bentuk 'aplikasi baik website maupun mobile.
    - {npc_nick}_{npc_role_m}: Selain 4 departemen, BCC juga terdapat 3 divisi yaitu Research and Development, Public Relation, dan Talent Development.
    - {npc_nick}_{npc_role_m}: Divisi Research and Development merupakan divisi yang bertanggung jawab dalam pengembangan riset yang dilakukan BCC ke mahasiswa UB maupun FILKOM.
    - {npc_nick}_{npc_role_m}: Divisi Public Relation adalah divisi yang memiliki peran untuk memastikan segala informasi dapat terkomunikasikan dengan baik antar anggota internal maupun external BCC.
    - {npc_nick}_{npc_role_m}: Terakhir terdapat divisi Talent Development dimana divisi ini yang mengatur dan membina anggota internal. Divisi ini juga menjadi pengawas bagi tiap departemen dalam BCC.
    - {npc_nick}_{npc_role_m}: Itu semua penjelasan singkat mengenai {booth_name}, apa sudah jelas?
    +   [Sudah Kak]
        {npc_nick}_{npc_role_m}: Bagus! Jangan lupa  daftar ke {booth_name} ya!
        {player_name}: Hehehe, siap kak saya pertimbangkan dulu.
    +   [Belum Kak]
        {npc_nick}_{npc_role_m}: Ok, aku jelaskan lagi ya. Simak dengan baik.
        -> Extra
    - {npc_nick}_{npc_role_m}: Mantap! Pertimbangkan dengan matang ya, kami siap menerima kamu kapanpun.
-> END
