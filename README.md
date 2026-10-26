\# BÁO CÁO BÀI TẬP MÔN: AN TOÀN VÀ BẢO MẬT THÔNG TIN



\* \*\*Tên sinh viên:\*\* Trần Đình Quang

\* \*\*Lớp:\*\* K59KTP

\* \*\*Mã số sinh viên:\*\* K235480106057

\* \*\*Hình thức:\*\* Báo cáo thực hành \& Mã nguồn ứng dụng (.NET 8 C#)

\* \*\*Repository:\*\* `gitff`



\---



\## MỤC LỤC

1\. \[Bài tập 1: Thuật toán mã hóa đối xứng AES](#1-bài-tập-1-thuật-toán-mã-hóa-đối-xứng-aes)

&#x20;  \* 1.1 Khởi tạo và Quản lý khóa AES

&#x20;  \* 1.2 Mã hóa dữ liệu văn bản (Text)

&#x20;  \* 1.3 Xử lý và Mã hóa tệp tin dung lượng lớn (100MB)

&#x20;  \* 1.4 Thực nghiệm và So sánh hiệu năng

2\. \[Bài tập 2: Thuật toán mã hóa bất đối xứng RSA và Chữ ký số](#2-bài-tập-2-thuật-toán-mã-hóa-bất-đối-xứng-rsa-và-chữ-ký-số)

&#x20;  \* 2.1 Khởi tạo cặp khóa và Phân tích định dạng XML

&#x20;  \* 2.2 Mã hóa và Giải mã dữ liệu khối nhỏ

&#x20;  \* 2.3 Xử lý File và Đánh giá tốc độ xử lý khóa

&#x20;  \* 2.4 Băm SHA-256 và Hiệu ứng tuyết lở (Avalanche Effect)

&#x20;  \* 2.5 Ứng dụng Chữ ký số và Đề xuất giải pháp ngẫu nhiên hóa (RSA-PSS)



\---



<a name="1-bài-tập-1-thuật-toán-mã-hóa-đối-xứng-aes"></a>

\## 1. Bài tập 1: Thuật toán mã hóa đối xứng AES



Dự án triển khai ứng dụng C# (.NET) thực hiện mã hóa và giải mã dữ liệu sử dụng chuẩn mã hóa đối xứng AES\[cite: 1]. Ứng dụng được thiết kế tối ưu bằng kỹ thuật Stream để xử lý mượt mà các file có dung lượng hàng Trăm Megabyte đến Gigabyte mà không gây tràn bộ nhớ\[cite: 1].



\### 1.1. Khởi tạo và Quản lý khóa AES

\- Ứng dụng tự động sinh khóa (Key) 256-bit (32 bytes) và vector khởi tạo (IV) 128-bit (16 bytes) ngẫu nhiên cho chuẩn AES thông qua lớp `Aes.Create()`\[cite: 1].

\- Tính năng xuất khóa cho phép mã hóa Key/IV sang định dạng Base64 và lưu thông tin ra file text cục bộ (`aes\_key.txt`) để tái sử dụng\[cite: 1].

\- Hỗ trợ tính năng Import khóa từ file đã lưu để tiến hành giải mã trên các phiên làm việc khác nhau\[cite: 1].



!\[Sinh khóa AES 256-bit và mã hóa text](ảnh%20AES%20và%20RSA/Hinh1\_SinhKhoa\_MaHoaText.png)

\*Giao diện VS Code và Console khởi tạo thành công khóa AES 256-bit, lưu khóa vào file `aes\_key.txt` và thực thi mã hóa đoạn văn bản thô ra dạng mã Base64.\*



\### 1.2. Mã hóa dữ liệu văn bản (Text)

\- Cho phép người dùng nhập trực tiếp một đoạn văn bản thô (Ví dụ: `"Day la bai tap cua Quang"`).

\- Hệ thống sử dụng khóa AES hiện tại để mã hóa văn bản và xuất trực tiếp kết quả mã hóa dưới dạng chuỗi Base64 ra màn hình UI/Console (Kết quả: `JTysww2e5B6d7gguD5JM7ZOc1emSHZ5Ou4ce6vX+4g4=`).



\### 1.3. Xử lý và Mã hóa tệp tin dung lượng lớn (100MB)

\- \*\*Cơ chế hoạt động:\*\* Ứng dụng đọc file nguồn thành từng khối nhỏ thông qua `FileStream` (với buffer 8192 bytes) và đẩy qua `CryptoStream`, sau đó ghi liên tiếp ra file đích\[cite: 1].

\- Cơ chế này đảm bảo bộ nhớ RAM chỉ tiêu tốn một lượng rất nhỏ\[cite: 1].

\- \*\*Biến đổi dung lượng do cơ chế đệm (PKCS7 Padding):\*\*

&#x20; - File gốc (`test\_file.dat`): Kích thước chính xác \*\*104.857.600 bytes\*\* (đúng 100 MB).

&#x20; - File mã hóa (`encrypted\_file.dat`): Kích thước \*\*104.857.616 bytes\*\* (Tăng 16 bytes tương ứng đúng 1 block AES do cơ chế đệm PKCS7).

&#x20; - File giải mã (`decrypted\_file.dat`): Kích thước \*\*104.857.600 bytes\*\* (Khôi phục nguyên vẹn 100% kích thước ban đầu).



!\[Properties File gốc và File mã hóa AES](ảnh%20AES%20và%20RSA/Hinh3\_Properties\_FileMaHoa.png)

\*So sánh File Properties của File gốc (104.857.600 bytes) và File đã mã hóa AES (104.857.616 bytes - chênh lệch tăng đúng 16 bytes Padding).\*



!\[Properties File gốc và File giải mã AES](ảnh%20AES%20và%20RSA/Hinh4\_Properties\_FileGiaiMa.png)

\*So sánh File Properties của File gốc và File đã giải mã decrypted\_file.dat (Khôi phục chính xác từng byte 104.857.600 bytes).\*



\### 1.4. Thực nghiệm và So sánh hiệu năng

\- Ứng dụng tích hợp bộ đếm thời gian (`Stopwatch`) để đo lường chính xác thời gian mã hóa và thời gian giải mã\[cite: 1].

&#x20; - Thời gian mã hóa file 100MB: \*\*\~284 ms\*\*.

&#x20; - Thời gian giải mã file 100MB: \*\*\~193 ms\*\*.

\- Cung cấp tính năng so sánh độ vẹn toàn của dữ liệu: Mở hai luồng đọc song song từ file gốc và file giải mã, tiến hành so sánh đối chiếu từng byte (`SequenceEqual`). Kết quả: \*\*Khớp 100%\*\*.



!\[Console log kết quả xử lý file AES](ảnh%20AES%20và%20RSA/Hinh2\_XuLyFile\_Console.png)

\*Màn hình Console log đo thời gian xử lý mã hóa/giải mã file mẫu 100MB và xác nhận kết quả kiểm tra tính toàn vẹn đạt "Khớp 100%".\*



> \*\*Kết luận thực nghiệm AES:\*\* Thời gian mã hóa và giải mã cực kỳ nhanh và gần như tương đương do bản chất đối xứng của thuật toán\[cite: 1]. Tốc độ phụ thuộc chủ yếu vào giới hạn đọc/ghi ổ cứng (Disk I/O) chứ không gây thắt cổ chai ở CPU hay RAM\[cite: 1].



\---



<a name="2-bài-tập-2-thuật-toán-mã-hóa-bất-đối-xứng-rsa-và-chữ-ký-số"></a>

\## 2. Bài tập 2: Thuật toán mã hóa bất đối xứng RSA và Chữ ký số



Mục tiêu của bài tập là ứng dụng cơ chế mã hóa bằng khóa công khai (Public Key) và giải mã bằng khóa bí mật (Private Key) thông qua thuật toán RSA-2048, kết hợp với các khái niệm về Băm dữ liệu (Hashing) và Chữ ký số\[cite: 1].



\### 2.1. Khởi tạo cặp khóa và Phân tích định dạng XML

\- Ứng dụng tự động sinh một cặp khóa RSA 2048-bit và xuất kết quả ra file chuẩn `.xml` (Chuẩn lưu trữ mặc định của .NET)\[cite: 1].

\- Phân tích cấu trúc file: 

&#x20; - Đọc file `rsa\_public.xml` để trích xuất thẻ `<Modulus>` (n) và `<Exponent>` (e = 65537)\[cite: 1].

&#x20; - Đọc file `rsa\_private.xml` để trích xuất đầy đủ các tham số bí mật: `n`, `e`, `<D>` (d), `<P>`, `<Q>`, v.v.\[cite: 1]



!\[File khóa public XML của RSA](ảnh%20AES%20và%20RSA/Hinh7\_RSA\_KeyFiles.png)

\*Giao diện VS Code mở file rsa\_public.xml hiển thị cấu trúc lưu trữ khóa công khai RSA dạng thẻ XML.\*



\### 2.2. Mã hóa và Giải mã dữ liệu khối nhỏ

\- \*\*Mã hóa:\*\* Chấp nhận đầu vào là một chuỗi văn bản (Ví dụ: `"Day la bai tap ma hoa RSA"`). Ứng dụng sử dụng Khóa công khai kết hợp chuẩn đệm an toàn `OAEP-SHA256` để mã hóa và lưu kết quả đầu ra dưới định dạng Base64.

\- \*\*Giải mã:\*\* Nhận đầu vào là chuỗi Base64 đã mã hóa. Tiến hành giải mã bằng Khóa bí mật và tự động so sánh tính toàn vẹn so với chuỗi gốc.



!\[Chạy RSA mã hóa chuỗi văn bản](ảnh%20AES%20và%20RSA/Hinh5\_RSA\_SinhKhoa\_Text.png)

\*Quá trình thực thi dự án RSA-2048, tự động nạp cặp khóa XML và mã hóa chuỗi văn bản bản rõ ra đoạn mã mã hóa Base64.\*



\### 2.3. Xử lý File và Đánh giá tốc độ xử lý khóa

\- Ứng dụng đọc file `rsa\_test\_file.txt`, mã hóa bằng Public Key và giải mã bằng Private Key sinh ra `rsa\_decrypted.txt`.

\- Đo lường tốc độ thực thi toán học trên hai khóa:

&#x20; - Sử dụng Public Key (tham số `e` rất nhỏ) cho tốc độ mã hóa nhanh (khoảng \*\*89 ms\*\*)\[cite: 1].

&#x20; - Sử dụng Private Key (tham số `d` có giá trị khổng lồ) tốn nhiều chi phí xử lý CPU hơn để giải mã (khoảng \*\*62 ms\*\* cho file văn bản nhỏ)\[cite: 1]. Khi áp dụng trên khối dữ liệu lớn, Private Key bộc lộ rõ tốc độ chậm hơn đáng kể so với Public Key và AES\[cite: 1].



!\[Console log mã hóa giải mã file RSA](ảnh%20AES%20và%20RSA/Hinh6\_RSA\_XuLyFile\_Console.png)

\*Màn hình Console ghi nhận kết quả thực thi toàn bộ Bài 2, đo thời gian xử lý file và xác nhận kết quả so sánh nội dung Khớp 100%.\*



!\[File giải mã RSA khôi phục bản rõ](ảnh%20AES%20và%20RSA/Hinh8\_RSA\_FileGiaiMa.png)

\*Giao diện VS Code hiển thị file rsa\_decrypted.txt sau khi giải mã, khôi phục chính xác 100% nội dung thông điệp gốc.\*



\### 2.4. Băm SHA-256 và Hiệu ứng tuyết lở (Avalanche Effect)

\- Ứng dụng cho phép nhập một chuỗi đầu vào và sinh ra mã băm (Hash) SHA-256 (64 ký tự Hex)\[cite: 1].

\- \*\*Đánh giá sự thay đổi:\*\* Khi thử nghiệm thay đổi dù chỉ một ký tự rất nhỏ ở đầu vào (ví dụ thêm 1 dấu chấm), chuỗi băm kết quả đầu ra sẽ thay đổi hoàn toàn (thay đổi ít nhất 50% số bit), minh chứng cho tính không thể đảo ngược và tính nhạy cảm của hàm băm\[cite: 1].



\### 2.5. Ứng dụng Chữ ký số và Đề xuất giải pháp ngẫu nhiên hóa

\- \*\*Nguyên lý chữ ký số:\*\* Sử dụng Private Key RSA để mã hóa chính chuỗi băm của dữ liệu đầu vào\[cite: 1]. Kết quả nhận được đóng vai trò là Chữ ký số của dữ liệu đó\[cite: 1]. Dữ liệu khác nhau sẽ sinh ra chữ ký số khác nhau\[cite: 1].

\- \*\*Giải quyết bài toán thực tế:\*\* Làm thế nào để "cùng 1 dữ liệu, cùng 1 người ký (cùng private key) thì chữ ký số sinh ra ở các lần ký khác nhau phải khác nhau"?\[cite: 1]

\- \*\*Phương pháp đề xuất:\*\* 

&#x20; Để phá vỡ tính tất định của thuật toán RSA cổ điển, tôi áp dụng phương pháp \*\*thêm giá trị nhiễu ngẫu nhiên (Salt/Timestamp/Padding)\*\*\[cite: 1]:

&#x20; 1. Mỗi lần ký, hệ thống tự động sinh một chuỗi ngẫu nhiên (Salt) hoặc lấy thời gian thực (Timestamp) ghép vào chuỗi dữ liệu gốc\[cite: 1].

&#x20; 2. Áp dụng hàm băm SHA-256 lên chuỗi dữ liệu đã được nối ghép này\[cite: 1].

&#x20; 3. Ký trên mã băm mới\[cite: 1]. Vì thành phần ngẫu nhiên thay đổi theo từng lần ký, mã băm sẽ khác nhau, dẫn đến chữ ký số đầu ra luôn luôn duy nhất dù nội dung cốt lõi không đổi\[cite: 1].

&#x20; 4. \*(Kỹ thuật thực tế phổ biến áp dụng nguyên lý này là chuẩn đệm PADDING RSA-PSS)\*\[cite: 1].

