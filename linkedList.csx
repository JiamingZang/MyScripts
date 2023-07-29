using System.Runtime.InteropServices;

unsafe struct Node
{
	public int num;
	public Node* next;
}

unsafe
{
	Node* create()
	{
		var head = (Node*)Marshal.AllocHGlobal(sizeof(Node));
		head->num = 0;
		head->next = null;
		return head;
	}

	void add(Node* head, int val)
	{
		var newNode = (Node*)Marshal.AllocHGlobal(sizeof(Node));
		newNode->num = val;
		newNode->next = null;
		Node* temp = head;
		while (temp->next != null)
		{
			temp = temp->next;
		}
		temp->next = newNode;
	}

	void printList(Node* head)
	{
		Node* temp = head;
		while (temp->next != null)
		{
			WriteLine(temp->num);
			temp = temp->next;
		}
		WriteLine(temp->num);
	}

	//head算是第0个，pos=2就是插在第一个和第三个之间
	void insert(Node* head, int val, int pos)
	{
		Node* temp = head;
		var newNode = (Node*)Marshal.AllocHGlobal(sizeof(Node));
		newNode->num = val;
		newNode->next = null;
		for (int i = 0; i < pos - 1; i++)
		{
			temp = temp->next;
		}
		newNode->next = temp->next;
		temp->next = newNode;
	}

	// 删除遇到的第一个num等于val的结点
	void deleteFirst(Node* head, int val)
	{
		Node* temp = head;
		while (temp->next != null)
		{
			if (temp->next->num == val)
			{
				temp->next = temp->next->next;
				return;
			}
			else
			{
				temp = temp->next;
			}
		}
	}

	var list = create();
	add(list, 1);
	add(list, 2);
	add(list, 3);
	printList(list);
	deleteFirst(list, 2);
	insert(list, 6, 2);
	printList(list);
}

