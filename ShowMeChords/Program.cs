using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

//класс акорда
class Chord
{
    private string name;//название em
    private string about;//описание

    private string imageGuitarURL;//аппликатура для гитары
    private string imageGuitarPhoto;//фото с постановкой на грифе для гитары

    private string imageUkuleleURL;//аппликатура для укулеле
    
    //конструктор
    public Chord(string chordsName, string aboutChord, string newGuitarImageUrl, string newImageGuitarGriffURL, string newUkuleleImageUrl = "")
    {

        name = chordsName;
        about = aboutChord;

        imageGuitarURL = newGuitarImageUrl;
        imageGuitarPhoto = newImageGuitarGriffURL;

        imageUkuleleURL = newUkuleleImageUrl;

    }

    //гет функции
    public string getImageGuitarURL() { return imageGuitarURL; }
    public string getImageGuitarGriffURL() { return imageGuitarPhoto; }

    public string getImageUkuleleURL() { return imageUkuleleURL; }
    public string getName() { return name; }
    public string getAbout() { return about; }
};


namespace ShowMeChords
{

    //доступные типы инструментов
    enum Instrument { Guitar, Ukulele };
    class Program
    {
        private static List<Chord> ChordsBase = new List<Chord>();//база аккордов
        //база выбора инструмента профилями, для того чтобы каждый мог выбрать свой инструмент и выбор одного профиля, не влиял на другого
        //первый параметр - id профиля который прислал смску
        //второй параметр - инструмент для профиля - гитара или укулеле
        private static Dictionary<long, Instrument> profilesInstruments = new Dictionary<long, Instrument>();
       
        //заполнение базы аккордов значениями
        static void InitializeChords()
        {
            ChordsBase.Clear();

            //---------------Варианты аккорда C-------------------------------
            ChordsBase.Add(new Chord("C", "C — мажорное трезвучие от ноты до Ноты: до, ми, соль", "https://upload.wikimedia.org/wikiversity/ru/9/92/Accord_C.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7d/Accord_C_photo.jpg/800px-Accord_C_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/ec7/ec7f783d85d11c586fbc03ef14423c0a.png"));
            ChordsBase.Add(new Chord("Cm", "Cm — минорное трезвучие от ноты до Ноты: до, ми-бемоль, соль", "https://upload.wikimedia.org/wikiversity/ru/0/0e/Accord_Cm.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/e/eb/Accord_Cm_photo.jpg/800px-Accord_Cm_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/bde/bdeecf56f198949e5064f324ad2ac6c9.png"));
            ChordsBase.Add(new Chord("C7", "C7 — малый мажорный септаккорд от ноты до Ноты: до, ми, соль, си-бемоль", "https://upload.wikimedia.org/wikiversity/ru/7/72/Accord_C7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6c/Accord_C7_photo.jpg/800px-Accord_C7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/0a6/0a600e422f36297877412bc16ce85971.png"));
            ChordsBase.Add(new Chord("Cm7", "Cm7 — малый минорный септаккорд от ноты до Ноты: до, ми-бемоль, соль, си-бемоль", "https://upload.wikimedia.org/wikiversity/ru/f/f2/Accord_Cm7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/46/Accord_Cm7_photo.jpg/800px-Accord_Cm7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/478/478e1b435ee79ad590e1657c4db323d4.png"));
            ChordsBase.Add(new Chord("Cmaj7", "Cmaj7 — большой мажорный септаккорд от ноты до Ноты: до, ми, соль, си", "https://upload.wikimedia.org/wikiversity/ru/8/8f/Accord_Cmaj7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/23/Accord_Cmaj7_photo.jpg/800px-Accord_Cmaj7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/dfa/dfa951a2bfeb9906d8a8269bf833333a.png"));
            ChordsBase.Add(new Chord("C#", "C# — мажорное трезвучие от ноты до-диез (ре-бемоль) Ноты: до-диез, ми-диез, соль-диез", "https://upload.wikimedia.org/wikiversity/ru/c/ca/Accord_C-diese.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/6/61/Accord_C-diese_photo.jpg/800px-Accord_C-diese_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/6f6/6f6cb5457fc4dd95353ff1d2e9e6ad60.png"));
            ChordsBase.Add(new Chord("C#m", "C#m — минорное трезвучие от ноты до-диез (ре-бемоль) Ноты: до-диез, ми, соль-диез", "https://upload.wikimedia.org/wikiversity/ru/0/05/Accord_C-diese-m.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/58/Accord_C-diese-m_photo.jpg/800px-Accord_C-diese-m_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/29b/29be1c885261aa76417ab93aad6d1419.png"));
            ChordsBase.Add(new Chord("C#7", "C#7 — малый мажорный септаккорд от ноты до-диез (ре-бемоль) Ноты: до-диез, ми-диез, соль-диез, си", "https://upload.wikimedia.org/wikiversity/ru/3/34/C-diese-7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e2/Accord_C-diese-7_photo.jpg/800px-Accord_C-diese-7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/a41/a41110c4c39d5af0e213ce9ce5903158.png"));
            ChordsBase.Add(new Chord("C#m7", "C#m7 — малый минорный септаккорд от ноты до-диез (ре-бемоль) Ноты: до-диез, ми, соль-диез, си", "https://upload.wikimedia.org/wikiversity/ru/f/fb/Accord_C-diese-m7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2d/Accord_C-diese-m7_photo.jpg/800px-Accord_C-diese-m7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/42f/42f967bd39da2c0299939e82a6c1f1ba.png"));

            //---------------Варианты аккорда D-------------------------------
            ChordsBase.Add(new Chord("D", "D — мажорное трезвучие от ноты ре Ноты: ре, фа-диез, ля", "https://upload.wikimedia.org/wikiversity/ru/8/86/Accord_D.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e7/Accord_D_photo.jpg/800px-Accord_D_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/b3d/b3d08abe8ed8bafc8b0a4638e6600dbd.png"));
            ChordsBase.Add(new Chord("Dm", "Dm — минорное трезвучие от ноты ре Ноты: ре, фа, ля", "https://upload.wikimedia.org/wikiversity/ru/5/51/Accord_Dm.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/Accord_Dm_photo.jpg/800px-Accord_Dm_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/050/05052fa10abad1a97f0a050ade6e6338.png"));
            ChordsBase.Add(new Chord("D7", "D7 — малый мажорный септаккорд от ноты ре Ноты: ре, фа-диез, ля, до", "https://upload.wikimedia.org/wikiversity/ru/c/cd/Accord_D7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/9/97/Accord_D7_photo.jpg/800px-Accord_D7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/aef/aef4506896a600c7da4cb5d707b73e2f.png"));
            ChordsBase.Add(new Chord("Dm7", "Dm7 — малый минорный септаккорд от ноты ре Ноты: ре, фа, ля, до", "https://upload.wikimedia.org/wikiversity/ru/a/af/Accord_Dm7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/be/Accord_Dm7_photo.jpg/800px-Accord_Dm7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/f92/f92ed831ab889ae51b637e37d568db59.png"));
            ChordsBase.Add(new Chord("Dm6", "Dm6 — минорное трезвучие с секстой от ноты ре Ноты: ре, фа, ля, до-дубль-бемоль", "https://upload.wikimedia.org/wikiversity/ru/f/f5/Accord_Dm6.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Accord_Dm6_photo.jpg/800px-Accord_Dm6_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/5cb/5cb822817eeea50333e5a6797e1ae6a2.png"));
            ChordsBase.Add(new Chord("D#", "D# — мажорное трезвучие от ноты ре-диез (ми-бемоль) Ноты: ре-диез, фа-дубль-диез, ля-диез", "https://upload.wikimedia.org/wikiversity/ru/3/34/Accord_D-diese.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f5/Accord_D-diese_photo.jpg/800px-Accord_D-diese_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/917/9179618c20b0e0f25aa923eb4ef811ab.png"));
            ChordsBase.Add(new Chord("D#m", "D#m — минорное трезвучие от ноты ре-диез (ми-бемоль) Ноты: ре-диез, фа-диез, ля-диез", "https://upload.wikimedia.org/wikiversity/ru/f/fd/Accord_D-diese-m.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1f/Accord_D-diese-m_photo.jpg/800px-Accord_D-diese-m_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/445/445ab111eef748cc710b7bfe1e7ea029.png"));
            ChordsBase.Add(new Chord("D#dim", "D#dim — уменьшенный септаккорд от ноты ре-диез (ми-бемоль) Ноты: ре-диез, фа-диез, ля, до", "https://upload.wikimedia.org/wikiversity/ru/c/c9/Accord_D-diese-dim.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f1/Accord_D-diese-dim_photo.jpg/800px-Accord_D-diese-dim_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/dc7/dc795350706196ddcea509227e0293e4.png"));
            
            //---------------Варианты аккорда E-------------------------------
            ChordsBase.Add(new Chord("E", "E — мажорное трезвучие от ноты ми Ноты: ми, соль-диез, си", "https://upload.wikimedia.org/wikiversity/ru/6/6c/Accord_E.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2b/Accord_E_photo.jpg/800px-Accord_E_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/385/385be6e6fb05430929b5acbff3f792c7.png"));
            ChordsBase.Add(new Chord("Em", "Em — минорное трезвучие от ноты ми Ноты: ми, соль, си", "https://upload.wikimedia.org/wikiversity/ru/4/40/Accord_Em.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Accord_Em_photo.jpg/800px-Accord_Em_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/5cb/5cb23240a0f9f10cddd578c3bb0ac9e9.png"));
            ChordsBase.Add(new Chord("E7", "E7 — малый мажорный септаккорд от ноты ми Ноты: ми, соль-диез, си, ре", "https://upload.wikimedia.org/wikiversity/ru/e/ee/Accord_E7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b7/Accord_E7_photo.jpg/800px-Accord_E7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/37c/37c1beea4a7d3d134749fd6b3d7b08b8.png"));
            ChordsBase.Add(new Chord("Em7", "Em7 — малый минорный септаккорд от ноты ми Ноты: ми, соль, си, ре", "https://upload.wikimedia.org/wikiversity/ru/d/d5/Accord_Em7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Accord_Em7_photo.jpg/800px-Accord_Em7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/c8e/c8e11273064594749535b0b6f7cfc6d1.png"));
            ChordsBase.Add(new Chord("E9", "E9 — большой нонаккорд от ноты ми Ноты: ми, соль-диез, си, ре, фа-диез", "https://upload.wikimedia.org/wikiversity/ru/8/8b/Accord_E9.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/14/Accord_E9_photo.jpg/800px-Accord_E9_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/8cf/8cff0e11514dde6df3d62a95e614f770.png"));

            //---------------Варианты аккорда F-------------------------------
            ChordsBase.Add(new Chord("F", "F — мажорное трезвучие от ноты фа Ноты: фа, ля, до", "https://upload.wikimedia.org/wikiversity/ru/2/2a/Accord_F.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/39/Accord_F_photo.jpg/800px-Accord_F_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/e86/e863be4e53a3caeb2d121f88e866523f.png"));
            ChordsBase.Add(new Chord("Fm", "Fm — минорное трезвучие от ноты фа Ноты: фа, ля-бемоль, до", "https://upload.wikimedia.org/wikiversity/ru/1/19/Accord_Fm.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/15/Accord_Fm_photo.jpg/800px-Accord_Fm_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/0a0/0a00c42ddc30bc24b153a14e58e0511a.png"));
            ChordsBase.Add(new Chord("F7", "F7 — малый мажорный септаккорд от ноты фа Ноты: фа, ля, до, ми-бемоль", "https://upload.wikimedia.org/wikiversity/ru/f/fc/Accord_F7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b7/Accord_F7_photo.jpg/800px-Accord_F7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/a31/a31ac4057e9377737a5d00e4435dbaa6.png"));
            ChordsBase.Add(new Chord("Fm7", "Fm7 — малый минорный септаккорд от ноты фа Ноты: фа, ля-бемоль, до, ми-бемоль", "https://upload.wikimedia.org/wikiversity/ru/7/7f/Accord_Fm7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/45/Accord_Fm7_photo.jpg/800px-Accord_Fm7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/067/0679be06290c26cdbcfd547bf9ccc250.png"));
            ChordsBase.Add(new Chord("F#", "F# — мажорное трезвучие от ноты фа-диез (соль-бемоль) Ноты: фа-диез, ля-диез, до-диез", "https://upload.wikimedia.org/wikiversity/ru/d/d7/Accord_F-diese.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a5/Accord_F-diese_photo.jpg/800px-Accord_F-diese_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/13d/13d2595e6a7791e7e82b2e02fd0b6f00.png"));
            ChordsBase.Add(new Chord("F#m", "F#m — минорное трезвучие от ноты фа-диез (соль-бемоль) Ноты: фа-диез, ля, до-диезез, ми", "https://upload.wikimedia.org/wikiversity/ru/a/a1/Accord_F-diese-m.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c3/Accord_F-diese-m_photo.jpg/800px-Accord_F-diese-m_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/e48/e48ea20a9a05ed704f91feb4b0a2d4db.png"));
            ChordsBase.Add(new Chord("F#7", "F#7 — малый мажорный септаккорд от ноты фа-диез (соль-бемоль) Ноты: фа-диез, ля-диез, до-диез, ми", "https://upload.wikimedia.org/wikiversity/ru/2/28/Accord_F-diese-7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Accord_F-diese-7_photo.jpg/800px-Accord_F-diese-7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/1f1/1f1810b9014b8cb862ff9bcfc9963fc9.png"));
            ChordsBase.Add(new Chord("F#m7", "F#m7 — малый минорный септаккорд от ноты фа-диез (соль-бемоль) Ноты: фа-диез, ля, до-диез, ми", "https://upload.wikimedia.org/wikiversity/ru/c/ce/Accord_F-diese-m7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/7/75/Accord_F-diese-m7_photo.jpg/800px-Accord_F-diese-m7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/c5d/c5d1a8bb5fa63310c876a91f8c517e65.png"));
            ChordsBase.Add(new Chord("F#dim", "F#dim — уменьшенный септаккорд от ноты фа-диез (соль-бемоль) Ноты: фа-диез, ля, до, ми-бемоль", "https://upload.wikimedia.org/wikiversity/ru/6/6b/Accord_F-diese-dim.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Accord_F-diese-dim_photo.jpg/800px-Accord_F-diese-dim_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/e48/e48ea20a9a05ed704f91feb4b0a2d4db.png"));
            
            //---------------Варианты аккорда G-------------------------------
            ChordsBase.Add(new Chord("G", "G — мажорное трезвучие от ноты соль Ноты: соль, си, ре", "https://upload.wikimedia.org/wikiversity/ru/a/a0/Accord_G.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cd/Accord_G_photo.jpg/800px-Accord_G_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/cea/cea76c7d1c73e069f19003297b46c826.png"));
            ChordsBase.Add(new Chord("Gm", "Gm — минорное трезвучие от ноты соль Ноты: соль, си-бемоль, ре", "https://upload.wikimedia.org/wikiversity/ru/3/36/Accord_Gm.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/8/88/Accord_Gm_photo.jpg/800px-Accord_Gm_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/1a0/1a0c846225996634a54aa9bd25546c89.png"));
            ChordsBase.Add(new Chord("G7", "G7 — малый мажорный септаккорд от ноты соль Ноты: соль, си, ре, фа", "https://upload.wikimedia.org/wikiversity/ru/c/c5/Accord_G7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/Accord_G7_photo.jpg/800px-Accord_G7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/f3b/f3b8fab2c550e251105aecc2e125c40e.png"));
            ChordsBase.Add(new Chord("Gm7", "Gm7 — малый минорный септаккорд от ноты соль Ноты: соль, си-бемоль, ре, фа", "https://upload.wikimedia.org/wikiversity/ru/c/ca/Accord_Gm7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/42/Accord_Gm7_photo.jpg/800px-Accord_Gm7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/4e5/4e528fc9852661680b434078fe00ced5.png"));
            ChordsBase.Add(new Chord("G#", "G# — мажорное трезвучие от ноты соль-диез (ля-бемоль) Ноты: соль-диез, си-диез, ре-диез", "https://upload.wikimedia.org/wikiversity/ru/8/80/Accord_G-diese.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/Accord_G-diese_photo.jpg/800px-Accord_G-diese_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/efd/efd1d872cde07a76fc442bd90a1753d9.png"));
            ChordsBase.Add(new Chord("G#m", "G#m — минорное трезвучие от ноты соль-диез (ля-бемоль) Ноты: соль-диез, си, ре-диез", "https://upload.wikimedia.org/wikiversity/ru/8/8f/Accord_G-diese-m.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/21/Accord_G-diese-m_photo.jpg/800px-Accord_G-diese-m_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/9db/9db9d9bf76221e135b370043ff1af969.png"));
            ChordsBase.Add(new Chord("G#7", "G#7 — малый мажорный септаккорд от ноты соль-диез (ля-бемоль) Ноты: соль-диез, си-диез, ре-диез, фа-диез", "https://upload.wikimedia.org/wikiversity/ru/3/32/Accord_G-diese-7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/11/Accord_G-diese-7_photo.jpg/800px-Accord_G-diese-7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/e91/e9166c5ad4ae5e6c2df5483f1e7d3209.png"));
            ChordsBase.Add(new Chord("G#m7", "G#m7 — малый минорный септаккорд от ноты соль-диез (ля-бемоль) Ноты: соль-диез, си, ре-диез, фа-диез", "https://upload.wikimedia.org/wikiversity/ru/4/4e/Accord_G-diese-m7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/9/94/Accord_G-diese-m7_photo.jpg/800px-Accord_G-diese-m7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/0a5/0a584126be67dd21cf27fd464f94dd8c.png"));

            //---------------Варианты аккорда A-------------------------------
            ChordsBase.Add(new Chord("A", "A — мажорное трезвучие от ноты ля Ноты: ля, до-диез, ми", "https://upload.wikimedia.org/wikiversity/ru/b/b1/Accord_A.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/de/Accord_A_photo.jpg/800px-Accord_A_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/87e/87e79b9b77a8bad1494be0bcefe253ab.png"));
            ChordsBase.Add(new Chord("Am", "Am — минорное трезвучие от ноты ля Ноты: ля, до, ми", "https://upload.wikimedia.org/wikiversity/ru/f/f7/Accord_Am.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2a/Accord_Am_photo.jpg/800px-Accord_Am_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/a19/a19205bf002c281322b8db61df09c9a5.png"));
            ChordsBase.Add(new Chord("A7", "A7 — малый мажорный септаккорд от ноты ля Ноты: ля, до-диез, ми, соль", "https://upload.wikimedia.org/wikiversity/ru/c/c2/Accord_A7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/9/90/Accord_A7_photo.jpg/800px-Accord_A7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/f1d/f1d1a721fd4aa87d60cf6d68a179fcb2.png"));
            ChordsBase.Add(new Chord("Am7", "Am7 — малый минорный септаккорд от ноты ля Ноты: ля, до, ми, соль", "https://upload.wikimedia.org/wikiversity/ru/7/72/Accord_Am7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/28/Accord_Am7_photo.jpg/800px-Accord_Am7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/866/866adaf0268cd037fcc711898f970121.png"));
            ChordsBase.Add(new Chord("A#", "A# — мажорное трезвучие от ноты ля-диез (си-бемоль) Ноты в составе аккорда: ля-диез, до-дубль-диез, ми-диез", "https://upload.wikimedia.org/wikiversity/ru/b/b6/Accord_A-diese.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7d/Accord_A-diese_photo.jpg/800px-Accord_A-diese_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/767/7673b7236d71dc4f430416a8f477fac9.png"));
            ChordsBase.Add(new Chord("A#m", "A#m — минорное трезвучие от ноты ля-диез (си-бемоль) Ноты: ля-диез, до-диез, ми-диез", "https://upload.wikimedia.org/wikiversity/ru/6/6c/Accord_A-diese-m.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/27/Accord_A-diese-m_photo.jpg/800px-Accord_A-diese-m_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/1b8/1b863980337c0ec12835ce0e35cef3cc.png"));
            ChordsBase.Add(new Chord("A#7", "A#7 — малый мажорный септаккорд от ноты ля-диез (си-бемоль) Ноты: ля-диез, до-дубль-диез, ми-диез, соль-диез", "https://upload.wikimedia.org/wikiversity/ru/3/31/Accord_A-diese-7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/Accord_A-diese-7_photo.jpg/800px-Accord_A-diese-7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/612/6129df555b703f824ce73ebb10a1a6c2.png"));
            ChordsBase.Add(new Chord("A#m7", "A#m7 — малый минорный септаккорд от ноты ля-диез (си-бемоль) Ноты: ля-диез, до-диез, ми-диез, соль-диез", "https://upload.wikimedia.org/wikiversity/ru/3/36/Accord_A-diese-m7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a8/Accord_A-diese-m7_photo.jpg/800px-Accord_A-diese-m7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/264/2645941e05f4ca4fd435a10deacfdb37.png"));

            //---------------Варианты аккорда H-------------------------------
            ChordsBase.Add(new Chord("H", "H — мажорное трезвучие от ноты си Ноты: си, ре-диез, фа-диез", "https://upload.wikimedia.org/wikiversity/ru/3/3f/Accord_H.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e4/Accord_H_photo.jpg/800px-Accord_H_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/085/085c555c76b454e50b85392654427dee.png"));
            ChordsBase.Add(new Chord("Hm", "Hm — минорное трезвучие от ноты си Ноты: си, ре, фа-диез", "https://upload.wikimedia.org/wikiversity/ru/4/42/Accord_Hm.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4f/Accord_Hm_photo.jpg/800px-Accord_Hm_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/456/4561243d8a32e8e6db884d69beed64d5.png"));
            ChordsBase.Add(new Chord("H7", "H7 — малый мажорный септаккорд от ноты си Ноты: си, ре-диез, фа-диез, ля", "https://upload.wikimedia.org/wikiversity/ru/e/ec/Accord_H7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Accord_H7_photo.jpg/800px-Accord_H7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/9ac/9ac9086f292bf30bbd47b36203095276.png"));
            ChordsBase.Add(new Chord("Hm7", "Hm7 — малый минорный септаккорд от ноты си Ноты: си, ре, фа-диез, ля", "https://upload.wikimedia.org/wikiversity/ru/d/de/Accord_Hm7.gif", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/37/Accord_Hm7_photo.jpg/800px-Accord_Hm7_photo.jpg", "https://opt-1289634.ssl.1c-bitrix-cdn.ru/upload/medialibrary/456/4561243d8a32e8e6db884d69beed64d5.png"));
        }

        //токен для бота
        private static string botToken  = "2006226874:AAFfY6iQVHr_eT9jkzw5c1lNm5YhXN_Qq1Y";
        //клиент телеграм бота
        private static TelegramBotClient client; 
        static void Main(string[] args)
        {

        InitializeChords();
           
        client = new TelegramBotClient(botToken);
        //начинаем слушать
        client.StartReceiving();
        client.OnMessage += OnMessageHandler;
        Console.WriteLine("Поехала жара.");
        Console.ReadLine();
        
        client.StopReceiving();




        }
            
            static private async void OnMessageHandler(object sender, MessageEventArgs e)//обработчик получения сообщения
            {

            var msg = e.Message;

            if (!profilesInstruments.ContainsKey(msg.From.Id))//если сообщение от профиля, которого еще нет в базе добавляем его
            {
                profilesInstruments.Add((msg.From.Id), new Instrument());
                Console.WriteLine("Был подключен новый профиль:" + msg.From.Id.ToString());
                Console.WriteLine("Количество профилей в базе: " + profilesInstruments.Count);
            }

            
            if (msg.Text != null)
                {

                switch(msg.Text)//проверяем не выбор ли это инструмента или первый вход
                {
                    case "Укулеле":
                        {
                            profilesInstruments[(msg.From.Id)] = Instrument.Ukulele;//меняем инструмент если была нажата эта кнопка
                            await client.SendTextMessageAsync(msg.Chat.Id, "Текущий инструмент: Укулеле.\n Напишите название аккорда(как угодно AM или am), чтобы посмотреть как он ставится.");
                            return;
                        }
                    case "Гитара":
                        {
                            profilesInstruments[(msg.From.Id)] = Instrument.Guitar;
                            await client.SendTextMessageAsync(msg.Chat.Id, "Текущий инструмент: Гитара.\nНапишите название аккорда(как угодно AM или am), чтобы посмотреть как он ставится.");
                            return;
                        }
                    case "/start"://первый вход
                        {
                            //Приветствуем и отправляем клавиатуру
                            await client.SendTextMessageAsync(msg.Chat.Id, "Привет. Здесь можно посмотреть аппликатуры аккордов для гитары и укулеле.\nМожно сменить инструмент:(по умолчанию - гитара)\nНапишите название аккорда(как угодно AM или am), чтобы посмотреть как он ставится.", replyMarkup: GetButtons());
                            
                            return;
                        }
                }




                
                for (int i = 0; i < ChordsBase.Count; i++)
                {
                    if (msg.Text.Equals(ChordsBase[i].getName(), StringComparison.OrdinalIgnoreCase))//приводим к нижнему регистру и сообщение и имя в базе и сравниваем 
                    {
                        
                        await client.SendTextMessageAsync(msg.Chat.Id, ChordsBase[i].getName() + "\n" + ChordsBase[i].getAbout(),replyToMessageId:msg.MessageId);
                        
                        if(profilesInstruments[msg.From.Id] == Instrument.Guitar)//если выбранный инструмнт данного профиля - гитара
                        { 
                        await client.SendPhotoAsync(msg.Chat.Id, ChordsBase[i].getImageGuitarURL());
                        await client.SendPhotoAsync(msg.Chat.Id, ChordsBase[i].getImageGuitarGriffURL());
                        }
                        else//если укулеле
                        {
                            await client.SendPhotoAsync(msg.Chat.Id, ChordsBase[i].getImageUkuleleURL());
                        }

                        return;
                    }
                }

                //если дошли до сюда, значит это не команда и не аккорд
                await client.SendTextMessageAsync(msg.Chat.Id, "Нет такого аккорда, возможно у вас там кот по клавиатуре пробежался. Попробуйте еще разок.");
                
            }
        
        }

        //создаем клавиатуру для отправки
        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Укулеле"}, new KeyboardButton { Text = "Гитара" } }
                }
            };
        }
    }

}
