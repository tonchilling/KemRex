IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'tmp_SysState')
BEGIN
	DROP TABLE [tmp_SysState];
END
GO
CREATE TABLE [tmp_SysState] (
	[StateId]		INT NOT NULL,
	[StateCode]		NVARCHAR(2) NULL,
	[StateNameTH]	NVARCHAR(256) NULL,
	[StateNameEN]	NVARCHAR(256) NULL,
	[GeoId]			INT NULL
);
GO
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (1, N'10', N'กรุงเทพฯ', N'Bangkok', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (2, N'11', N'สมุทรปราการ', N'Samutprakan', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (3, N'12', N'นนทบุรี', N'Nonthaburi', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (4, N'13', N'ปทุมธานี', N'Pathum thani', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (5, N'14', N'พระนครศรีอยุธยา', N'Ayutthaya', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (6, N'15', N'อ่างทอง', N'Angthong', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (7, N'16', N'ลพบุรี', N'Lopburi', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (8, N'17', N'สิงห์บุรี', N'Singburi', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (9, N'18', N'ชัยนาท', N'Chai nat', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (10, N'19', N'สระบุรี', N'Saraburi', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (11, N'20', N'ชลบุรี', N'Chon buri', 5);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (12, N'21', N'ระยอง', N'Rayong', 5);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (13, N'22', N'จันทบุรี', N'Chanthaburi', 5);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (14, N'23', N'ตราด', N'Trat', 5);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (15, N'24', N'ฉะเชิงเทรา', N'Chachoengsao', 5);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (16, N'25', N'ปราจีนบุรี', N'Prachinburi', 5);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (17, N'26', N'นครนายก', N'Nakhon nayok', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (18, N'27', N'สระแก้ว', N'Sra kaeo', 5);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (19, N'30', N'นครราชสีมา', N'Nakhon ratchasima', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (20, N'31', N'บุรีรัมย์', N'Buri ram', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (21, N'32', N'สุรินทร์', N'Surin', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (22, N'33', N'ศรีสะเกษ', N'Srisaket', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (23, N'34', N'อุบลราชธานี', N'Uboratchathani', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (24, N'35', N'ยโสธร', N'Yasothon', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (25, N'36', N'ชัยภูมิ', N'Chai yaphum', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (26, N'37', N'อำนาจเจริญ', N'Amnatcharoen', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (27, N'39', N'หนองบัวลำภู', N'Nongbualamphu', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (28, N'40', N'ขอนแก่น', N'Khon kean', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (29, N'41', N'อุดรธานี', N'Udonthani', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (30, N'42', N'เลย', N'Loei', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (31, N'43', N'หนองคาย', N'Nongkhai', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (32, N'44', N'มหาสารคาม', N'Mahasarakham', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (33, N'45', N'ร้อยเอ็ด', N'Roi et', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (34, N'46', N'กาฬสินธุ์', N'Kalasin', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (35, N'47', N'สกลนคร', N'Sakonnakhon', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (36, N'48', N'นครพนม', N'Nakhon phanom', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (37, N'49', N'มุกดาหาร', N'Mukdahan', 3);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (38, N'50', N'เชียงใหม่', N'Chiang mai', 1);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (39, N'51', N'ลำพูน', N'Lamphun', 1);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (40, N'52', N'ลำปาง', N'Lampang', 1);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (41, N'53', N'อุตรดิตถ์', N'Uttaradit', 1);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (42, N'54', N'แพร่', N'Phrae', 1);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (43, N'55', N'น่าน', N'Nan', 1);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (44, N'56', N'พะเยา', N'Phayao', 1);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (45, N'57', N'เชียงราย', N'Chiang rai', 1);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (46, N'58', N'แม่ฮ่องสอน', N'Mae hong son', 1);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (47, N'60', N'นครสวรรค์', N'Nakhon sawan', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (48, N'61', N'อุทัยธานี', N'Uthaithani', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (49, N'62', N'กำแพงเพชร', N'Kamphaeng phet', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (50, N'63', N'ตาก', N'Tak', 4);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (51, N'64', N'สุโขทัย', N'Sukhothai', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (52, N'65', N'พิษณุโลก', N'Phitsanulok', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (53, N'66', N'พิจิตร', N'phichit', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (54, N'67', N'เพชรบูรณ์', N'Phetchabun', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (55, N'70', N'ราชบุรี', N'Rachaburi', 4);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (56, N'71', N'กาญจนบุรี', N'Kanchanaburi', 4);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (57, N'72', N'สุพรรณบุรี', N'Suphanburi', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (58, N'73', N'นครปฐม', N'Nakhon pathom', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (59, N'74', N'สมุทรสาคร', N'Samut sakhon', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (60, N'75', N'สมุทรสงคราม', N'Samut songkhram', 2);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (61, N'76', N'เพชรบุรี', N'Phetchaburi', 4);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (62, N'77', N'ประจวบคีรีขันธ์', N'Prachuap khiri khan', 4);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (63, N'80', N'นครศรีธรรมราช', N'Nakhon si thammarat', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (64, N'81', N'กระบี่', N'Krabi', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (65, N'82', N'พังงา', N'Phangnga', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (66, N'83', N'ภูเก็ต', N'Phuket', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (67, N'84', N'สุราษฎร์ธานี', N'Surat thani', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (68, N'85', N'ระนอง', N'Ranong', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (69, N'86', N'ชุมพร', N'Chum phon', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (70, N'90', N'สงขลา', N'Songkhla', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (71, N'91', N'สตูล', N'Satun', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (72, N'92', N'ตรัง', N'Trang', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (73, N'93', N'พัทลุง', N'Phatthalung', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (74, N'94', N'ปัตตานี', N'Pattani', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (75, N'95', N'ยะลา', N'Yala', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (76, N'96', N'นราธิวาส', N'Narathiwat', 6);
INSERT [tmp_SysState] ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId]) VALUES (77, N'97', N'บึงกาฬ', N'Bueng Kan', 3);
GO
SET IDENTITY_INSERT [SysState] ON;
GO
MERGE INTO [SysState] AS trg USING (
	SELECT
		[StateId],
		[StateCode],
		[StateNameTH],
		[StateNameEN],
		[GeoId]
	FROM [tmp_SysState]) AS src
ON trg.[StateId] = src.[StateId]
WHEN NOT MATCHED THEN
	INSERT ([StateId], [StateCode], [StateNameTH], [StateNameEN], [GeoId])
	VALUES (src.[StateId], src.[StateCode], src.[StateNameTH], src.[StateNameEN], src.[GeoId])
WHEN MATCHED THEN
	UPDATE SET
		trg.[StateCode] = src.[StateCode],
		trg.[StateNameTH] = src.[StateNameTH],
		trg.[StateNameEN] = src.[StateNameEN],
		trg.[GeoId] = src.[GeoId];
GO
SET IDENTITY_INSERT [SysState] OFF;
GO
DROP TABLE [tmp_SysState];
GO