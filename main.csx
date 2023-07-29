#r "nuget: Newtonsoft.Json, 13.0.3"
#load "book.csx"

using System.Numerics;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
// public class Book
// {
// 	public string Title { get; set; }
// 	public string Author { get; set; }
// }

var book = new Book() { Title = "易筋经", Author = "达摩没有院" };
WriteLine(JsonConvert.SerializeObject(book));

var books = new List<Book>();

books.Add(book);

foreach (var item in books)
{
	WriteLine(item.Title.ToString());
}

Vector3 position = new(0.0f, 0.0f, 0.0f);
WriteLine(position.ToString());

unsafe struct Node
{
	public int num;
	public Vector3 position;
	public Node* next;
}
unsafe
{
	var head = (Node*)Marshal.AllocHGlobal(sizeof(Node));
	head->num = 1;
	head->position = new(0.0f, 0.0f, 0.0f);
	var newnode = (Node*)Marshal.AllocHGlobal(sizeof(Node));
	newnode->num = 2;
	newnode->position = new(1.0f, 3.0f, 0.0f);
	head->next = newnode;

	WriteLine(head->num);
	WriteLine(head->next->num);
	WriteLine(head->next->position);

	static void Test1(ref Node book)
	{
		WriteLine(book.position.Y);
	}

	Test1(ref *head);
}

class MemoryStorage : IDisposable
{
	private IntPtr _pointer;
	public int Size { get; }

	public IntPtr Data { get => _pointer; }

	public unsafe Span<Byte> DataSpan { get => new((void*)_pointer, Size); }

	public MemoryStorage(int size)
	{
		_pointer = Marshal.AllocHGlobal(size);
		GC.AddMemoryPressure(size);
		Size = size;
	}
	~MemoryStorage()
	{
		Dispose();
	}

	public void Dispose()
	{
		if (_pointer != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(_pointer);
			GC.RemoveMemoryPressure(Size);
			_pointer = IntPtr.Zero;
		}
	}
}

void Test()
{
	using MemoryStorage m = new(100);
	WriteLine(m.Size);
	var span = m.DataSpan;
	span.Fill(0xFF);
	WriteLine(span[0]);
}

Test();
long Test(int num)
{
	long sum = 0;
	for (int i = 0; i < num; i++)
	{
		sum += i;
	}
	return sum;
}

Stopwatch sw = new();
sw.Start();
Console.WriteLine(Test(10000000));
sw.Stop();
Console.WriteLine("耗时:" + sw.Elapsed.TotalSeconds.ToString("0.000") + "s");

