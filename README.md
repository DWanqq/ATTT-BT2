# BÁO CÁO BÀI TẬP MÔN: AN TOÀN VÀ BẢO MẬT THÔNG TIN

*   **Tên sinh viên:** Trần Đình Quang
*   **Lớp:** K59KTP
*   **Mã số sinh viên:** K235480106057
*   **Hình thức:** Báo cáo thực hành & Mã nguồn ứng dụng (.NET)

---

## MỤC LỤC
1. [Bài tập 1: Thuật toán mã hóa đối xứng AES](#bai-tap-1-thuat-toan-ma-hoa-doi-xung-aes)
   * 1.1 Khởi tạo và Quản lý khóa AES
   * 1.2 Mã hóa dữ liệu văn bản (Text)
   * 1.3 Xử lý và Mã hóa tệp tin dung lượng lớn (GB)
   * 1.4 Thực nghiệm và So sánh hiệu năng
2. [Bài tập 2: Thuật toán mã hóa bất đối xứng RSA và Chữ ký số](#bai-tap-2-thuat-toan-ma-hoa-bat-doi-xung-rsa-va-chu-ky-so)
   * 2.1 Khởi tạo cặp khóa và Phân tích định dạng PEM
   * 2.2 Mã hóa và Giải mã dữ liệu khối nhỏ
   * 2.3 Đánh giá tốc độ xử lý khóa Public vs Private
   * 2.4 Băm SHA-256 và Hiệu ứng tuyết lở (Avalanche Effect)
   * 2.5 Ứng dụng Chữ ký số và Đề xuất giải pháp ngẫu nhiên hóa

---

<a name="bai-tap-1-thuat-toan-ma-hoa-doi-xung-aes"></a>
## 1. Bài tập 1: Thuật toán mã hóa đối xứng AES

Dự án triển khai ứng dụng C# (.NET) thực hiện mã hóa và giải mã dữ liệu sử dụng chuẩn mã hóa đối xứng AES[cite: 1]. Ứng dụng được thiết kế tối ưu bằng kỹ thuật Stream để xử lý mượt mà các file có dung lượng hàng Gigabyte.

### 1.1. Khởi tạo và Quản lý khóa AES
- Ứng dụng tự động sinh khóa (Key) và vector khởi tạo (IV) ngẫu nhiên cho chuẩn AES 256-bit[cite: 1].
- Tính năng xuất khóa cho phép lưu thông tin khóa ra file text cục bộ để tái sử dụng[cite: 1].
- Hỗ trợ tính năng Import khóa từ file đã lưu để tiến hành giải mã trên các phiên làm việc khác nhau[cite: 1].

> `[KÉO THẢ ẢNH VÀO ĐÂY: Chụp màn hình giao diện sinh khóa và màn hình Notepad mở file chứa khóa AES đã lưu]`

### 1.2. Mã hóa dữ liệu văn bản (Text)
- Cho phép người dùng nhập trực tiếp một đoạn văn bản thô[cite: 1].
- Hệ thống sử dụng khóa AES hiện tại để mã hóa văn bản và xuất trực tiếp kết quả mã hóa (dưới dạng chuỗi Base64) ra màn hình UI/Console[cite: 1].

> `[KÉO THẢ ẢNH VÀO ĐÂY: Chụp màn hình lúc nhập văn bản và in ra chuỗi mã hóa kết quả]`

### 1.3. Xử lý và Mã hóa tệp tin dung lượng lớn (GB)
- **Cơ chế hoạt động:** Ứng dụng đọc file nguồn thành từng khối nhỏ (chunk) thông qua `FileStream` và đẩy qua `CryptoStream`, sau đó ghi liên tiếp ra file đích[cite: 1].
- Cơ chế này đảm bảo bộ nhớ RAM chỉ tiêu tốn một lượng rất nhỏ (bằng kích thước buffer) dù file gốc lên tới hàng GB[cite: 1].

> `[KÉO THẢ ẢNH VÀO ĐÂY: Chụp cửa sổ Properties (hiển thị dung lượng) của file gốc hàng GB, file đã mã hóa và file đã giải mã]`

### 1.4. Thực nghiệm và So sánh hiệu năng
- Ứng dụng tích hợp bộ đếm thời gian (Stopwatch) để đo lường chính xác thời gian mã hóa và thời gian giải mã[cite: 1].
- Cung cấp tính năng so sánh độ vẹn toàn của dữ liệu: Mở hai luồng đọc song song từ file gốc và file giải mã, tiến hành so sánh đối chiếu từng byte (Compare theo bytes)[cite: 1].

> `[KÉO THẢ ẢNH VÀO ĐÂY: Chụp giao diện hiển thị Log kết quả: "Thời gian mã hóa: X giây", "Thời gian giải mã: Y giây", "Compare Bytes: Khớp 100%"]`

**Kết luận thực nghiệm AES:**
Thời gian mã hóa và giải mã gần như tương đương do bản chất đối xứng của thuật toán. Tốc độ phụ thuộc chủ yếu vào giới hạn đọc/ghi ổ cứng (Disk I/O) chứ không gây thắt cổ chai ở CPU hay RAM.

---

<a name="bai-tap-2-thuat-toan-ma-hoa-bat-doi-xung-rsa-va-chu-ky-so"></a>
## 2. Bài tập 2: Thuật toán mã hóa bất đối xứng RSA và Chữ ký số

Mục tiêu của bài tập là ứng dụng cơ chế mã hóa bằng khóa công khai (Public Key) và giải mã bằng khóa bí mật (Private Key) thông qua thuật toán RSA, kết hợp với các khái niệm về Băm dữ liệu (Hashing) và Chữ ký số[cite: 1].

### 2.1. Khởi tạo cặp khóa và Phân tích định dạng PEM
- Ứng dụng tự động sinh một cặp khóa RSA và xuất kết quả ra file chuẩn `.pem`[cite: 1].
- Phân tích cấu trúc file: 
  - Đọc file Public Key PEM để trích xuất hai tham số: Modulus (`n`) và Public Exponent (`e`)[cite: 1].
  - Đọc file Private Key PEM để trích xuất đầy đủ các tham số: `n`, `e`, Private Exponent (`d`), và các tham số khác[cite: 1].

> `[KÉO THẢ ẢNH VÀO ĐÂY: Chụp màn hình Notepad mở 2 file public.pem và private.pem]`

### 2.2. Mã hóa và Giải mã dữ liệu khối nhỏ
- **Mã hóa:** Chấp nhận đầu vào là một mảng byte có kích thước dưới 190 byte. Ứng dụng sử dụng khóa vừa import để mã hóa dữ liệu này và lưu kết quả đầu ra dưới định dạng Base64 vào file[cite: 1].
- **Giải mã:** Nhận đầu vào là file chứa chuỗi Base64 đã mã hóa và file chứa khóa PEM tương ứng. Tiến hành giải mã và tự động so sánh tính toàn vẹn so với chuỗi gốc[cite: 1].

> `[KÉO THẢ ẢNH VÀO ĐÂY: Chụp giao diện hiển thị chuỗi dữ liệu gốc, chuỗi mã hóa (Base64) và kết quả so sánh giải mã]`

### 2.3. Đánh giá tốc độ xử lý khóa Public vs Private
- Đo lường và so sánh trực tiếp tốc độ thực thi toán học trên hai khóa:
  - Sử dụng Public Key (thường có tham số `e` rất nhỏ, ví dụ: 65537) cho tốc độ tính toán cực kỳ nhanh[cite: 1].
  - Sử dụng Private Key (tham số `d` có giá trị khổng lồ) tốn nhiều chi phí xử lý CPU hơn, dẫn đến tốc độ chậm hơn đáng kể[cite: 1].

> `[KÉO THẢ ẢNH VÀO ĐÂY: Chụp bảng log tốc độ trên màn hình hiển thị chênh lệch thời gian giữa Public Key và Private Key]`

### 2.4. Băm SHA-256 và Hiệu ứng tuyết lở (Avalanche Effect)
- Ứng dụng cho phép nhập một chuỗi đầu vào và sinh ra mã băm (Hash) SHA-256[cite: 1].
- **Đánh giá sự thay đổi:** Khi thử nghiệm thay đổi dù chỉ một ký tự rất nhỏ ở đầu vào, chuỗi băm kết quả đầu ra sẽ thay đổi hoàn toàn, minh chứng cho tính không thể đảo ngược và tính nhạy cảm của hàm băm[cite: 1].

> `[KÉO THẢ ẢNH VÀO ĐÂY: Chụp giao diện hiển thị 2 đoạn text đầu vào (chỉ khác nhau 1 dấu chấm/chữ cái) và 2 chuỗi Hash SHA-256 khác biệt hoàn toàn]`

### 2.5. Ứng dụng Chữ ký số và Đề xuất giải pháp ngẫu nhiên hóa
- **Nguyên lý chữ ký số:** Sử dụng Private Key RSA để mã hóa chính chuỗi băm của dữ liệu đầu vào. Kết quả nhận được đóng vai trò là Chữ ký số của dữ liệu đó[cite: 1]. Dữ liệu khác nhau sẽ sinh ra chữ ký số khác nhau[cite: 1].
- **Giải quyết bài toán thực tế:** Làm thế nào để "cùng 1 dữ liệu, cùng 1 người ký (cùng private key) thì chữ ký số sinh ra ở các lần ký khác nhau phải khác nhau"?[cite: 1]
- **Phương pháp đề xuất:** 
  Để phá vỡ tính tất định của thuật toán RSA, tôi áp dụng phương pháp **thêm giá trị nhiễu ngẫu nhiên (Salt/Timestamp/Padding)**:
  1. Mỗi lần ký, hệ thống tự động sinh một chuỗi ngẫu nhiên (Salt) hoặc lấy thời gian thực (Timestamp) ghép vào chuỗi dữ liệu gốc.
  2. Áp dụng hàm băm SHA-256 lên chuỗi dữ liệu đã được nối ghép này.
  3. Ký trên mã băm mới. Vì thành phần ngẫu nhiên thay đổi theo từng lần ký, mã băm sẽ khác nhau, dẫn đến chữ ký số đầu ra luôn luôn duy nhất dù nội dung cốt lõi không đổi.
  4. (Kỹ thuật thực tế phổ biến áp dụng nguyên lý này là chuẩn PADDING RSA-PSS).
