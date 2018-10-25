using System;
using System.Collections;
using System.Threading;

namespace Reactor
{
	/// <summary>
	/// Summary description for EventQueue.
	/// </summary>
	public class EventQueue
	{
		private Queue				m_item_queue;
		private Queue				m_wait_queue;
		//private bool				m_stopping;

		public EventQueue()
		{
			//m_stopping = false;
			m_item_queue = new Queue();
			m_wait_queue = new Queue();
		}

		public int itemQsize()
		{
			return m_item_queue.Count;
		}	

		public void Shutdown()
		{
			lock(this)
			{
				//m_stopping = true;
				m_item_queue.Clear();
				m_wait_queue.Clear();
			}
		}

		public void putQ(object o)
		{
			lock(this)
			{
				//if(m_stopping)
					//return;

				// Put an item on the queue. If there are any receivers waiting
				// dispatch the item immediately. Otherwise add to the queue
				if(m_wait_queue.Count > 0)
				{
					// pop off the first waiter from the wait queue
					// and dispatch the item
					AsyncWork op = (AsyncWork)m_wait_queue.Dequeue();

					// plug the item into the operation and post the lot 
					// to the threadpool
					ThreadPool.QueueUserWorkItem(op.m_waitCallback, o);
				}
				else
				{
					m_item_queue.Enqueue(o);
				}
			}
		}

		public void getQ(AsyncWork op)
		{
			lock(this)
			{
				//if(m_stopping)
					//return;

				// Retrieve an item from the queue, to avoid sequencing
				// problems the item has to go through the completion port

				// 8/16/2006 NOT CLEAR WHY THIS IS THE CASE COMMENTS SHOULD EXPLAIN HOW 
				// SEQUENCING PROBLEMS CAN ARISE. This will dispatch the item in the queue 
				// on another thread from the threadpool. This does avoid a long or busy queue
				// from permanently hogging one thread from the threadpool by having it continually
				// draining the queue
				if(m_item_queue.Count > 0)
				{
					// pop off the first item from the item queue
					// and dispatch the item
					object o = m_item_queue.Dequeue();

					// plug the item into the operation and post the lot 
					// to the threadpool
					ThreadPool.QueueUserWorkItem(op.m_waitCallback, o);
				}
				else
				{
					m_wait_queue.Enqueue(op);
				}
			}
		}
	}
}
