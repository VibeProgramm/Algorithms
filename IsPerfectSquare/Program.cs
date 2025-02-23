// See https://aka.ms/new-console-template for more information

if (IsPerfectSquare(160000) == false) throw new Exception("Ожидалось что будет true");
if (IsPerfectSquare(160001)) throw new Exception("Ожидалось что будет false");
return;

// реализация через сдвиги байтов
bool IsPerfectSquare(int n) {
    if (n < 0) return false;
        
    long bits = BitConverter.DoubleToInt64Bits(n);
    bits = ((bits - (1L << 52)) >> 1) + (1L << 61);
    double approxDouble = BitConverter.Int64BitsToDouble(bits);

    // 2) Одна итерация Ньютона для уточнения:
    //    x_{k+1} = 0.5 * ( x_k + n / x_k )
    approxDouble = (0.5 * (approxDouble + n / approxDouble));

    // 3) Приводим к int и проверяем квадрат
    int approx = (int)approxDouble;

    // Проверяем вокруг approx, чтобы нивелировать дробные погрешности
    return approx * approx == n
           || (approx + 1) * (approx + 1) == n
           || (approx - 1) * (approx - 1) == n;
}