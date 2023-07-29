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
	deleteFirst(list, 3);
	deleteFirst(list, 1);
	printList(list);
}

